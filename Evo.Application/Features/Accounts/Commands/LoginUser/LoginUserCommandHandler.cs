using AutoMapper;
using Evo.Application.Contracts.Persistence;
using Evo.Application.Contracts.Security;
using Evo.Application.Features.Accounts.Dtos;
using Evo.Domain.Enums;
using MediatR;
using System.Security.Cryptography;
using System.Text;

namespace Evo.Application.Features.Accounts.Commands.LoginUser
{
    public class LoginUserCommandHandler(IUserRepository _userRepository, ITokenService _tokenService, IMapper _mapper) : IRequestHandler<LoginUserCommand, UserDto>
    {
 

        public async Task<UserDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            // 1️⃣ Get user by email, include navigation properties
            var user = await _userRepository.GetByEmailAsync(request.LoginDto.Email);
            if (user == null || !user.IsActive)
                throw new System.Exception("Invalid email or inactive account.");

            // 2️⃣ Verify password
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.LoginDto.Password));
            if (!computedHash.SequenceEqual(user.PasswordHash))
                throw new System.Exception("Invalid password");

            // 3️⃣ Update last login
            user.LastLogin = System.DateTime.UtcNow;
            await _userRepository.UpdateAsync(user);

            // 4️⃣ Determine role dynamically for JWT / UserDto
            string role;

            if (user.RolePermissions == Evo.Domain.Enums.UserRole.Admin)
            {
                // Check navigation property to see if staff is superadmin
                if (user.Staff?.Admin != null)
                {
                    role = user.Staff.Admin.Position == AdminPosition.SuperAdmin
                        ? "SuperAdmin"
                        : "Admin";
                }
                else
                {
                    // fallback
                    role = "Admin";
                }
            }
            else
            {
                // Other roles (Customer, ServiceProvider, ThirdPartyDriver)
                role = user.RolePermissions.ToString();
            }

            // 5️⃣ Map to UserDto and generate token
            var dto = new UserDto
            {
                Id = user.Id,
                DisplayName = user.DisplayName, // dynamically resolved from navigation
                Email = user.Email,
                Role = role,
                Token = _tokenService.CreateToken(user) // token should include this role claim
            };

            return dto;
        }
    }
}
