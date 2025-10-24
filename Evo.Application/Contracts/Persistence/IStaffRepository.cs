using Evo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evo.Application.Contracts.Persistence
{
    public interface IStaffRepository
    {
        Task AddAsync(Staff satff);
        Task<Staff?> GetByIdAsync(string id);
        Task<IEnumerable<Staff>> GetAllAsync();
    }
}
