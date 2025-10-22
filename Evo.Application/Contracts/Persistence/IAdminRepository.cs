using Evo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evo.Application.Contracts.Persistence
{
    public interface IAdminRepository
    {
        Task AddAsync(Admin admin);
        Task<Admin?> GetByIdAsync(int id);
        Task<IEnumerable<Admin>> GetAllAsync();
    }
}
