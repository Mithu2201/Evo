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
    public class AdminRepository(AppDbContext _context) : IAdminRepository
    {
        public async Task AddAsync(Admin admin) =>
            await _context.Admins.AddAsync(admin);

        public async Task<Admin?> GetByIdAsync(string id) =>
            await _context.Admins.FirstOrDefaultAsync(a => a.Id == id);

        public async Task<IEnumerable<Admin>> GetAllAsync() =>
            await _context.Admins.ToListAsync();

        public async Task<Admin?> GetByStaffIdAsync(string staffId) =>
            await _context.Admins.FirstOrDefaultAsync(a => a.StaffId == staffId);
    }
}
