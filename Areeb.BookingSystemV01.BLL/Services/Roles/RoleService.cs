using Areeb.BookingSystem.DAL.Entities.Roles;
using Areeb.BookingSystemV01.BLL.Models.Roles;
using Areeb.BookingSystemV01.DAL.Persistences.Repositories.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Areeb.BookingSystemV01.BLL.Services.Roles
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<IEnumerable<RoleReadDto>> GetAllAsync()
        {
            var roles = await _roleRepository.GetAllAsync();
            return roles.Select(r => new RoleReadDto
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description
            });
        }

        public async Task<RoleReadDto?> GetByIdAsync(int id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role == null) return null;

            return new RoleReadDto
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description
            };
        }

        public async Task AddAsync(RoleCreateDto dto)
        {
            var role = new Role
            {
                Name = dto.Name,
                Description = dto.Description
            };
            await _roleRepository.AddAsync(role);
            await _roleRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, RoleUpdateDto dto)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role == null) return;

            role.Name = dto.Name;
            role.Description = dto.Description;

            await _roleRepository.UpdateAsync(role);
            await _roleRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _roleRepository.DeleteAsync(id);
            await _roleRepository.SaveChangesAsync();
        }
    }

}
