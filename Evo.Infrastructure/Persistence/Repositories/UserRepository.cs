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
    public class UserRepository(AppDbContext _context) : IUserRepository
    {
        public async Task<bool> UserExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.Customer)
                .Include(u => u.ServiceProvider)
                .Include(u => u.Staff)
                    .ThenInclude(s => s.Admin) // if Staff has an Admin navigation
                .Include(u => u.ThirdPartyDriver)
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }



        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        // ✅ New UpdateAsync method
        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }


    }
}
