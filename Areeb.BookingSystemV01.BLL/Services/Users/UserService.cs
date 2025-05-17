using Areeb.BookingSystem.DAL.Entities.Users;
using Areeb.BookingSystemV01.BLL.Models.Users;
using Areeb.BookingSystemV01.DAL.Persistences.Repositories.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Areeb.BookingSystemV01.BLL.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<IEnumerable<UserReadDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => new UserReadDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                RoleName = u.Role?.Name,
                CreatedAt = u.CreatedAt
            });
        }

        public async Task<UserReadDto?> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null;

            return new UserReadDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                RoleName = user.Role?.Name,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task AddAsync(UserCreateDto dto)
        {
            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Password = dto.Password, 
                RoleId = dto.RoleId
            };
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, UserUpdateDto dto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return;

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Email = dto.Email;
            user.Password = dto.Password;
            user.RoleId = dto.RoleId;

            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();
        }
        

        public string GetCurrentUserId()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
        public async Task DeleteAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
            await _userRepository.SaveChangesAsync();
        }
    }

}
