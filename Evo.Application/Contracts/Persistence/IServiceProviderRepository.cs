using Evo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evo.Application.Contracts.Persistence
{
    public interface IServiceProviderRepository
    {
        Task AddAsync(ServiceProvider serviceProvider);
        Task<ServiceProvider?> GetByIdAsync(int id);
        Task<IEnumerable<ServiceProvider>> GetAllAsync();
    }
}
