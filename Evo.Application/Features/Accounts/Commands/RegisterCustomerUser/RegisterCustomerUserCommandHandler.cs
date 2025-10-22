using AutoMapper;
using Evo.Application.Contracts.Persistence;
using Evo.Application.Contracts.Security;
using Evo.Application.Features.Accounts.Dtos;
using Evo.Application.Features.Accounts.Events.Notification;
using Evo.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Evo.Application.Features.Accounts.Commands.RegisterCustomerUser
{
    public class RegisterCustomerUserCommandHandler(IMapper _mapper, ITokenService _tokenService, IUnitOfWork _unitOfWork, IMediator _mediator) : IRequestHandler<RegisterCustomerUserCommand, UserDto>
    {
        public async Task<UserDto> Handle(RegisterCustomerUserCommand request, CancellationToken cancellationToken)
        {
            // 1️ Check if email already exists
            if (await _unitOfWork.Users.UserExistsAsync(request.RegisterCustomerUserDto.Email))
                throw new Exception("Email already registered");

            // 2️ Begin transaction
            await _unitOfWork.BeginTransactionAsync();

            try
            {

                // 3️ Map DTO to User entity
                var user = _mapper.Map<User>(request.RegisterCustomerUserDto);
               

                // 4️ Add user
                await _unitOfWork.Users.AddAsync(user);


                // 5️ Add corresponding customer

                var customerEventDto = _mapper.Map<CustomerUserRegisteredDto>(request.RegisterCustomerUserDto);
                customerEventDto.UserId = user.Id;

               

                var customer = _mapper.Map<Customer>(customerEventDto);
                await _unitOfWork.Customers.AddAsync(customer);




                // 6️ Commit transaction
                await _unitOfWork.CommitTransactionAsync();

                // 7️ Map to DTO and create token
                var userDto = _mapper.Map<UserDto>(user);
                userDto.Token = _tokenService.CreateToken(user);

                return userDto;
            }
            catch
            {
                // 8️ Rollback on error
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
