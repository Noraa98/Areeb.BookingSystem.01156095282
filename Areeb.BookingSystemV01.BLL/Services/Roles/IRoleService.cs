using Areeb.BookingSystemV01.BLL.Models.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Areeb.BookingSystemV01.BLL.Services.Roles
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleReadDto>> GetAllAsync();
        Task<RoleReadDto?> GetByIdAsync(int id);
        Task AddAsync(RoleCreateDto dto);
        Task UpdateAsync(int id, RoleUpdateDto dto);
        Task DeleteAsync(int id);
    }

}
