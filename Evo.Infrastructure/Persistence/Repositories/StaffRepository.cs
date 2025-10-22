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

            public async Task<Staff?> GetByIdAsync(int id) =>
                await _context.Staffs.FindAsync(id);

            public async Task<IEnumerable<Staff>> GetAllAsync() =>
                await _context.Staffs.ToListAsync();

        
    }
}
