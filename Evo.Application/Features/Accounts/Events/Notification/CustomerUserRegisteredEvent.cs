using Evo.Application.Features.Accounts.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evo.Application.Features.Accounts.Events.Notification
{
    // Event triggered after a user is successfully registered
    // Encapsulates all user info needed for subsequent operations (e.g., creating a customer)
    public record CustomerUserRegisteredEvent(CustomerUserRegisteredDto User) : INotification;
}
