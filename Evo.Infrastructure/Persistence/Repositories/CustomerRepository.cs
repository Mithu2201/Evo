using Evo.Application.Contracts.Persistence;
using Evo.Domain.Entities;
using Evo.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evo.Infrastructure.Persistence.Repositories
{
    public class CustomerRepository(AppDbContext _context) : ICustomerRepository
    {
        public async Task AddAsync(Customer customer) =>
            await _context.Customers.AddAsync(customer);

        public async Task<Customer?> GetByIdAsync(string id)
        {
            return await _context.Customers
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == id && c.Status != AccountStatus.Deleted);
        }

        // 📋 Get all active customers (with User Include)
        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers
                .Include(c => c.User)
                .Where(c => c.Status != AccountStatus.Deleted)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        // ✏️ Update existing customer
        public async Task UpdateAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await Task.CompletedTask;
        }

        // 🗑️ Soft delete (set status to Deleted instead of removing)
        public async Task SoftDeleteAsync(string id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
            if (customer is null)
                throw new KeyNotFoundException($"Customer with ID '{id}' not found.");

            customer.Status = AccountStatus.Deleted;
            customer.UpdatedAt = DateTime.UtcNow;

            _context.Customers.Update(customer);
        }

        // 💾 Save changes (commonly used with Unit of Work)
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
