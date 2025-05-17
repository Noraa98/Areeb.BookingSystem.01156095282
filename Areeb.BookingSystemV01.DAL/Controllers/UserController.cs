using Areeb.BookingSystemV01.BLL.Models.Users;
using Areeb.BookingSystemV01.BLL.Services.Roles;
using Areeb.BookingSystemV01.BLL.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Areeb.BookingSystemV01.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public UserController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllAsync();
            return View(users);
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();
            return View(user);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Roles = await _roleService.GetAllAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserCreateDto userDto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = await _roleService.GetAllAsync();
                return View(userDto);
            }

            await _userService.AddAsync(userDto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();

            var roles = await _roleService.GetAllAsync();
            ViewBag.Roles = roles;

            var userUpdateDto = new UserUpdateDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                RoleId = user.RoleId
            };

            return View(userUpdateDto);
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, UserUpdateDto userDto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = await _roleService.GetAllAsync();
                return View(userDto);
            }

            await _userService.UpdateAsync(id, userDto);
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
