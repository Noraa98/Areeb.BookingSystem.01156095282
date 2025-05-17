using Areeb.BookingSystemV01.BLL.Models.Roles;
using Areeb.BookingSystemV01.BLL.Services.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Areeb.BookingSystemV01.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleService.GetAllAsync();
            return View(roles);
        }

        public async Task<IActionResult> Details(int id)
        {
            var role = await _roleService.GetByIdAsync(id);
            if (role == null) return NotFound();
            return View(role);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleCreateDto roleDto)
        {
            if (!ModelState.IsValid)
                return View(roleDto);

            await _roleService.AddAsync(roleDto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var role = await _roleService.GetByIdAsync(id);
            if (role == null) return NotFound();

            var roleUpdateDto = new RoleUpdateDto
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description
            };

            return View(roleUpdateDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, RoleUpdateDto roleDto)
        {
            if (!ModelState.IsValid)
                return View(roleDto);

            await _roleService.UpdateAsync(id, roleDto);
            return RedirectToAction(nameof(Index));
        }


        // GET: Role/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var role = await _roleService.GetByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        // POST: Role/DeleteConfirmed/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _roleService.DeleteAsync(id);
            TempData["Success"] = "Role deleted successfully.";
            return RedirectToAction(nameof(Index));
        }

    }
}
