using Evo.Application.Contracts.Persistence;
using Evo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evo.Infrastructure.Persistence.Repositories
{
    public class ServiceProviderRepository(AppDbContext _context) : IServiceProviderRepository
    {
        public async Task AddAsync(ServiceProvider serviceProvider) =>
            await _context.ServiceProviders.AddAsync(serviceProvider);

        public async Task<ServiceProvider?> GetByIdAsync(int id) =>
            await _context.ServiceProviders.FindAsync(id);

        public async Task<IEnumerable<ServiceProvider>> GetAllAsync() =>
            await _context.ServiceProviders.ToListAsync();
    }
}
