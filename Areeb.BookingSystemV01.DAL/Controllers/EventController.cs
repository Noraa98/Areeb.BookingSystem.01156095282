using Areeb.BookingSystemV01.BLL.Models.Events;
using Areeb.BookingSystemV01.BLL.Services.Bookings;
using Areeb.BookingSystemV01.BLL.Services.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Areeb.BookingSystemV01.PL.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        public async Task<IActionResult> Index()
        {
            var events = await _eventService.GetAllAsync();
            return View(events);
        }

        public async Task<IActionResult> Details(int id)
        {
            var @event = await _eventService.GetByIdAsync(id);
            if (@event == null) return NotFound();
            return View(@event);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EventCreateDto eventDto)
        {
            if (!ModelState.IsValid)
                return View(eventDto);

            await _eventService.AddAsync(eventDto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var @event = await _eventService.GetByIdAsync(id);
            if (@event == null) return NotFound();

            var eventUpdateDto = new EventUpdateDto
            {
                Id = @event.Id,
                EventName = @event.EventName,
                Description = @event.Description,
                Category = @event.Category,
                Location = @event.Location,
                EventDate = @event.EventDate,
                Venue = @event.Venue,
                Price = @event.Price,
                ImageUrl = @event.ImageUrl,
                IsActive = @event.IsActive
            };

            return View(eventUpdateDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EventUpdateDto eventDto)
        {
            if (id != eventDto.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(eventDto);
            }

            await _eventService.UpdateAsync(id, eventDto);

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _eventService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
    

