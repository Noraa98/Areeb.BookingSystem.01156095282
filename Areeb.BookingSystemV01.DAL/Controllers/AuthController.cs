//using Areeb.BookingSystemV01.BLL.Services.Roles;
//using Areeb.BookingSystemV01.BLL.Services.Users;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System.Security.Claims;

//namespace Areeb.BookingSystemV01.PL.Controllers
//{
//    public class AuthController : Controller
//    {
//        private readonly IUserService _userService;
//        private readonly IRoleService _roleService;

//        public AuthController(IUserService userService, IRoleService roleService)
//        {
//            _userService = userService;
//            _roleService = roleService;
//        }

//        [HttpGet]
//        public IActionResult Register()
//        {
//            return View();
//        }

//        [HttpPost]
//        public async Task<IActionResult> Register(UserRegisterDto dto)
//        {
//            if (!ModelState.IsValid)
//                return View(dto);

//            await _userService.RegisterAsync(dto);
//            return RedirectToAction("Login");
//        }

//        [HttpGet]
//        public IActionResult Login()
//        {
//            return View();
//        }

//        [HttpPost]
//        public async Task<IActionResult> Login(UserLoginDto dto)
//        {
//            if (!ModelState.IsValid)
//                return View(dto);

//            var user = await _userService.ValidateUserAsync(dto.Email, dto.Password);
//            if (user == null)
//            {
//                ModelState.AddModelError("", "Invalid email or password.");
//                return View(dto);
//            }

//            var role = await _roleService.GetByIdAsync(user.RoleId);

//            var claims = new List<Claim>
//            {
//                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
//                new Claim(ClaimTypes.Name, user.Email ?? ""),
//                new Claim(ClaimTypes.Role, role?.Name ?? "User")
//            };

//            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
//            var principal = new ClaimsPrincipal(identity);

//            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
//            return RedirectToAction("Index", "Home");
//        }

//        [Authorize]
//        public async Task<IActionResult> Logout()
//        {
//            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
//            return RedirectToAction("Login");
//        }
//    }
//}
