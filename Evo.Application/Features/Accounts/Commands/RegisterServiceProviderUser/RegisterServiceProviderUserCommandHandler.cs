using AutoMapper;
using Evo.Application.Contracts.Persistence;
using Evo.Application.Contracts.Security;
using Evo.Application.Features.Accounts.Dtos;
using Evo.Domain.Entities;
using Evo.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evo.Application.Features.Accounts.Commands.RegisterServiceProviderUser
{
    public class RegisterServiceProviderUserCommandHandler(IMapper _mapper, ITokenService _tokenService, IUnitOfWork _unitOfWork, IMediator _mediator) : IRequestHandler<RegisterServiceProviderUserCommand, UserDto>
    {
        public async Task<UserDto> Handle(RegisterServiceProviderUserCommand request, CancellationToken cancellationToken)
        {
            var dto = request.RegisterServiceProviderUserDto;

            // 1) Email uniqueness (User)
            if (await _unitOfWork.Users.UserExistsAsync(dto.Email))
                throw new Exception("Email already registered");

            // 2) Begin transaction
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                // 3) Map DTO -> User
                var user = _mapper.Map<User>(dto);

                // Optional safety defaults if your mapper doesn't already do this

                if (dto.IsActive is null)
                    user.IsActive = true;

                // 4) Persist User
                await _unitOfWork.Users.AddAsync(user);

                // 5) Map DTO -> ServiceProvider
                var serviceProvider = _mapper.Map<ServiceProvider>(dto);

                // Link the new user
                serviceProvider.UserId = user.Id;

                // If provider email not explicitly provided, mirror User.Email
                serviceProvider.Email ??= dto.Email;

                // Default verification if not provided via mapping
                if (!Enum.IsDefined(typeof(VerificationStatus), serviceProvider.VerificationStatus))
                    serviceProvider.VerificationStatus = VerificationStatus.Pending;

                // 6) Persist ServiceProvider
                await _unitOfWork.ServiceProviders.AddAsync(serviceProvider);

                // 7) Commit transaction
                await _unitOfWork.CommitTransactionAsync();

                // 8) Map to UserDto + issue JWT
                var userDto = _mapper.Map<UserDto>(user);
                userDto.Token = _tokenService.CreateToken(user);

                return userDto;
            }
            catch
            {
                // 9) Rollback on error
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
