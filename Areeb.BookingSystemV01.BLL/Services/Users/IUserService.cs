using Areeb.BookingSystemV01.BLL.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Areeb.BookingSystemV01.BLL.Services.Users
{
    public interface IUserService
    {
        Task<IEnumerable<UserReadDto>> GetAllAsync();
        Task<UserReadDto?> GetByIdAsync(int id);
        Task AddAsync(UserCreateDto dto);
        Task UpdateAsync(int id, UserUpdateDto dto);
        Task DeleteAsync(int id);

        string GetCurrentUserId();
    }
}
