using AutoMapper;
using Evo.Application.Contracts.Persistence;
using Evo.Application.Features.Accounts.Events.Notification;
using Evo.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evo.Application.Features.Accounts.Events.Handlers
{
    // Handles the UserRegisteredEvent.
    // This runs after a user is registered and adds a corresponding Customer record.

    public class CreateCustomerOnUserRegisteredHandler(IUnitOfWork _unitOfWork,IMapper _mapper) : INotificationHandler<CustomerUserRegisteredEvent>
    {
        public async Task Handle(CustomerUserRegisteredEvent notification, CancellationToken cancellationToken)
        {
            var customer = _mapper.Map<Customer>(notification.User);
            await _unitOfWork.Customers.AddAsync(customer);

        }

    }
}
