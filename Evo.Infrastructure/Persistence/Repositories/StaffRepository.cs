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
    public class StaffRepository(AppDbContext _context) : IStaffRepository
    {
        public async Task AddAsync(Staff staff) =>
            await _context.Staffs.AddAsync(staff);

        public async Task<Staff?> GetByIdAsync(string id) =>
            await _context.Staffs.FirstOrDefaultAsync(s => s.Id == id);

        public async Task<IEnumerable<Staff>> GetAllAsync() =>
            await _context.Staffs.ToListAsync();

        public async Task<Staff?> GetStaffByUserIdAsync(string userId)
        {
            return await _context.Staffs
                .Include(s => s.User)
                .Include(s => s.Admin)
                .FirstOrDefaultAsync(s => s.UserId == userId);
        }
    }
}
