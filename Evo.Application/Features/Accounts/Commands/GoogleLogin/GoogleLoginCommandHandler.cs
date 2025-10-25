using AutoMapper;
using Evo.Application.Contracts.Persistence;
using Evo.Application.Contracts.Security;
using Evo.Application.Features.Accounts.Dtos;
using Evo.Domain.Entities;
using Evo.Domain.Enums;
using MediatR;
using System.Security.Cryptography;
using System.Text;

namespace Evo.Application.Features.Accounts.Commands.GoogleLogin
{
    public class GoogleLoginCommandHandler(
        IGoogleAuthService _googleAuthService,
        ITokenService _tokenService,
        IUnitOfWork _unitOfWork,
        IMapper _mapper)
        : IRequestHandler<GoogleLoginCommand, UserDto>
    {
        public async Task<UserDto> Handle(GoogleLoginCommand request, CancellationToken cancellationToken)
        {
            // Validate Google ID token and extract user info
            var googleInfo = await _googleAuthService.ValidateIdTokenAsync(request.IdToken, audience: null, cancellationToken);

            // Try get existing user by email
            var user = await _unitOfWork.Users.GetByEmailAsync(googleInfo.Email);

            if (user is null)
            {
                // Create a new local user and related customer
                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    // Create random password to satisfy required fields
                    using var hmac = new HMACSHA512();
                    var salt = hmac.Key;
                    var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString("N")));

                    user = new User
                    {
                        Email = googleInfo.Email,
                        PasswordSalt = salt,
                        PasswordHash = hash,
                        RolePermissions = UserRole.Customer,
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow,
                        LastLogin = DateTime.UtcNow,
                    };

                    await _unitOfWork.Users.AddAsync(user);

                    var customer = new Customer
                    {
                        UserId = user.Id,
                        Name = string.IsNullOrWhiteSpace(googleInfo.Name) ? googleInfo.Email : googleInfo.Name,
                        Email = googleInfo.Email,
                        Status = AccountStatus.Active,
                        CreatedAt = DateTime.UtcNow
                    };

                    await _unitOfWork.Customers.AddAsync(customer);
                    // ensure navigation available for token/map
                    user.Customer = customer;

                    await _unitOfWork.CommitTransactionAsync();
                }
                catch
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    throw;
                }
            }
            else
            {
                // Update last login for existing user
                user.LastLogin = DateTime.UtcNow;
                await _unitOfWork.SaveChangesAsync();
            }

            var userDto = _mapper.Map<UserDto>(user);
            userDto.Token = _tokenService.CreateToken(user);
            return userDto;
        }
    }
}
