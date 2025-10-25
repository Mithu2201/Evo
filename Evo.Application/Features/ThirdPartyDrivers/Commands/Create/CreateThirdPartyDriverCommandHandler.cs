using AutoMapper;
using Evo.Application.Contracts.Persistence;
using Evo.Application.Features.ThirdPartyDrivers.Dtos;
using Evo.Domain.Entities;
using Evo.Domain.Enums;
using MediatR;
using System.Security.Cryptography;
using System.Text;

namespace Evo.Application.Features.ThirdPartyDrivers.Commands.Create
{
    public class CreateThirdPartyDriverCommandHandler : IRequestHandler<CreateThirdPartyDriverCommand, ThirdPartyDriverDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CreateThirdPartyDriverCommandHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ThirdPartyDriverDto> Handle(CreateThirdPartyDriverCommand request, CancellationToken cancellationToken)
        {
            await _uow.BeginTransactionAsync();

            try
            {
                var entity = _mapper.Map<ThirdPartyDriver>(request.Driver);

                // If a UserId is provided but doesn't exist, create a new User based on driver details
                if (!string.IsNullOrWhiteSpace(entity.UserId))
                {
                    var existing = await _uow.Users.GetUserByIdAsync(entity.UserId);
                    if (existing is null)
                    {
                        // Create minimal User record for this driver
                        var user = new User
                        {
                            Email = entity.WorkEmail,
                            RolePermissions = UserRole.ThirdParty,
                            IsActive = true,
                        };

                        // Generate a temporary random password (the driver should reset it later)
                        var tempPassword = $"Drv-{Guid.NewGuid():N}!Aa1";
                        using var hmac = new HMACSHA512();
                        user.PasswordSalt = hmac.Key;
                        user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(tempPassword));

                        await _uow.Users.AddAsync(user);
                        // link newly created user to driver (override provided id to ensure validity)
                        entity.UserId = user.Id;
                    }
                }

                await _uow.ThirdPartyDrivers.AddAsync(entity, cancellationToken);
                await _uow.CommitTransactionAsync();

                return _mapper.Map<ThirdPartyDriverDto>(entity);
            }
            catch
            {
                await _uow.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
