using Evo.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Evo.Application.Contracts.Persistence
{
    public interface IAdminRepository
    {
        Task AddAsync(Admin admin);
        Task<Admin?> GetByIdAsync(string id);
        Task<IEnumerable<Admin>> GetAllAsync();
        Task<Admin?> GetByStaffIdAsync(string staffId);
    }
}
