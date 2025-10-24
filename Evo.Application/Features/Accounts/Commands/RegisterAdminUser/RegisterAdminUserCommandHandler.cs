using AutoMapper;
using Evo.Application.Contracts.Persistence;
using Evo.Application.Contracts.Security;
using Evo.Application.Features.Accounts.Dtos;
using Evo.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Evo.Application.Features.Accounts.Commands.RegisterAdminUser
{
    public class RegisterAdminUserCommandHandler(IMapper _mapper, ITokenService _tokenService, IUnitOfWork _unitOfWork, IMediator _mediator) : IRequestHandler<RegisterAdminUserCommand, UserDto>
    {
        public async Task<UserDto> Handle(RegisterAdminUserCommand request, CancellationToken cancellationToken)
        {
            // Promote an existing Staff to Admin using StaffId
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var dto = request.RegisterAdminUserDto;

                // 1) Validate Staff exists
                var staff = await _unitOfWork.Staffs.GetByIdAsync(dto.StaffId);
                if (staff is null)
                    throw new Exception("Staff not found for the given StaffId.");

                // 2) Ensure no existing Admin row for this staff
                var existingAdmin = await _unitOfWork.Admins.GetByStaffIdAsync(staff.Id);
                if (existingAdmin is not null)
                    throw new Exception("This staff member is already an admin.");

                // 3) Create Admin linked to Staff
                var admin = new Admin
                {
                    StaffId = staff.Id,
                    Status = dto.Status,
                    Position = dto.Position
                };
                await _unitOfWork.Admins.AddAsync(admin);

                // 4) Elevate the associated User role to Admin
                var user = await _unitOfWork.Users.GetUserByIdAsync(staff.UserId);
                if (user is null)
                    throw new Exception("User not found for the given staff.");
                user.RolePermissions = Evo.Domain.Enums.UserRole.Admin;

                // 5) Commit changes
                await _unitOfWork.CommitTransactionAsync();

                // 6) Return mapped UserDto + token
                var userDto = _mapper.Map<UserDto>(user);
                //userDto.Token = _tokenService.CreateToken(user);
                return userDto;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}

