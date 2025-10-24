using Evo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evo.Application.Contracts.Persistence
{
    public interface IUserRepository
    {
        Task<bool> UserExistsAsync(string email);
        Task AddAsync(User user);
        Task<User?> GetByEmailAsync(string email);
        Task<List<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(string id);

        // ✅ Add this method
        Task UpdateAsync(User user);

    }
}
