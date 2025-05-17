using Areeb.BookingSystemV01.BLL.Models.Bookings;
using Areeb.BookingSystemV01.BLL.Services.Bookings;
using Areeb.BookingSystemV01.BLL.Services.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Areeb.BookingSystemV01.PL.Controllers
{

    [Authorize]
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IEventService _eventService;

        public BookingController(
            IBookingService bookingService,
            IEventService eventService)
        {
            _bookingService = bookingService;
            _eventService = eventService;
        }

        public async Task<IActionResult> Index()
        {
            int? userId = GetUserIdFromClaims();
            if (userId == null)
                return Unauthorized();

            var bookings = await _bookingService.GetByUserIdAsync(userId.Value);
            return View(bookings);
        }

        /// [HttpPost]
        /// public async Task<IActionResult> Create(int eventId)
        /// {
        ///     int? userId = GetUserIdFromClaims();
        ///     if (userId == null)
        ///         return Unauthorized();
        /// 
        ///     var alreadyBooked = await _bookingService.HasUserBookedEventAsync(userId.Value, eventId);
        ///     if (alreadyBooked)
        ///     {
        ///         TempData["Error"] = "لقد قمت بحجز هذه الفعالية من قبل.";
        ///         return RedirectToAction("Details", "Events", new { id = eventId });
        ///     }
        /// 
        ///     var bookingDto = new BookingCreateDto
        ///     {
        ///         UserId = userId.Value,
        ///         EventId = eventId
        ///     };
        /// 
        ///     await _bookingService.AddAsync(bookingDto);
        /// 
        ///     return RedirectToAction("Confirmation", new { eventId });
        /// }

        public async Task<IActionResult> Confirmation(int eventId)
        {
            var @event = await _eventService.GetByIdAsync(eventId);
            if (@event == null)
                return NotFound();

            return View(@event);
        }

        [HttpPost]
        public async Task<IActionResult> Cancel(int id)
        {
            int? userId = GetUserIdFromClaims();
            if (userId == null)
                return Unauthorized();

            var booking = await _bookingService.GetByIdAsync(id);
            if (booking == null)
                return NotFound();

            if (booking.Id != userId && !User.IsInRole("Admin"))
                return Forbid();

            await _bookingService.DeleteAsync(id);
            TempData["Success"] = "تم إلغاء الحجز بنجاح.";

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Book(int eventId)
        {
            // 1. نجيب الـ UserId من الـ HttpContext (مثلاً من الـ Claims بعد تسجيل الدخول)
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                // لو المستخدم مش مسجل دخول
                return RedirectToAction("Login", "Account");
            }

            int userId = int.Parse(userIdClaim.Value);

            // 2. نتحقق لو المستخدم حجز الفعالية قبل كده (تجنب الحجز المكرر)
            bool alreadyBooked = await _bookingService.HasUserBookedEventAsync(userId, eventId);
            if (alreadyBooked)
            {
                // ممكن ترجع رسالة أو توجيه لصفحة معينة
                TempData["Message"] = "You have already booked this event.";
                return RedirectToAction("Index", "Event");
            }

            // 3. نعمل حجز جديد
            var bookingDto = new BookingCreateDto
            {
                UserId = userId,
                EventId = eventId,
                BookingDate = DateTime.Now
            };

            await _bookingService.AddAsync(bookingDto);

            // 4. بعد الحجز نوجه المستخدم لصفحة Confirmation
            return RedirectToAction("Confirmation", new { eventId });
        }


        private int? GetUserIdFromClaims()
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(userIdString, out var userId) ? userId : null;
        }
    }
}
