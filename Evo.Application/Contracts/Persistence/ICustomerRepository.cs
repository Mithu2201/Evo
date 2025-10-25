using Evo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evo.Application.Contracts.Persistence
{
    // ICustomerRepository.cs
    public interface ICustomerRepository
    {
        Task AddAsync(Customer customer);
        // 🔍 Get customer by ID (excluding soft-deleted)
        Task<Customer?> GetByIdAsync(string id);

        // 📋 Get all active customers
        Task<IEnumerable<Customer>> GetAllAsync();

        // ✏️ Update existing customer
        Task UpdateAsync(Customer customer);

        // 🗑️ Soft delete customer
        Task SoftDeleteAsync(string id);

        // 💾 Commit changes
        Task SaveChangesAsync();
    }
}
