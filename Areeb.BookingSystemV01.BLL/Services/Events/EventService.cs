using Areeb.BookingSystem.DAL.Entities.Events;
using Areeb.BookingSystemV01.BLL.Models.Events;
using Areeb.BookingSystemV01.DAL.Persistences.Repositories.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Areeb.BookingSystemV01.BLL.Services.Events
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<IEnumerable<EventReadDto>> GetAllAsync()
        {
            var events = await _eventRepository.GetAllAsync();
            return events.Select(e => new EventReadDto
            {
                Id = e.Id,
                EventName = e.EventName,
                Description = e.Description,
                Category = e.Category,
                Location = e.Location,
                Price = e.Price,
                EventDate = e.EventDate,
                Venue = e.Venue,
                IsActive = e.IsActive,
                ImageUrl = e.ImageUrl
            });
        }

        public async Task<EventReadDto?> GetByIdAsync(int id)
        {
            var e = await _eventRepository.GetByIdAsync(id);
            if (e == null) return null;

            return new EventReadDto
            {
                Id = e.Id,
                EventName = e.EventName,
                Description = e.Description,
                Category = e.Category,
                Location = e.Location,
                Price = e.Price,
                EventDate = e.EventDate,
                Venue = e.Venue,
                IsActive = e.IsActive,
                ImageUrl = e.ImageUrl
            };
        }

        public async Task AddAsync(EventCreateDto dto)
        {
            var ev = new Event
            {
                EventName = dto.EventName,
                Description = dto.Description,
                Category = dto.Category,
                Location = dto.Location,
                Price = dto.Price,
                EventDate = dto.EventDate,
                Venue = dto.Venue,
                ImageUrl = dto.ImageUrl
            };
            await _eventRepository.AddAsync(ev);
            await _eventRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, EventUpdateDto dto)
        {
            var existing = await _eventRepository.GetByIdAsync(id);
            if (existing == null) return;

            existing.EventName = dto.EventName;
            existing.Description = dto.Description;
            existing.Category = dto.Category;
            existing.Location = dto.Location;
            existing.Price = dto.Price;
            existing.EventDate = dto.EventDate;
            existing.Venue = dto.Venue;
            existing.ImageUrl = dto.ImageUrl;
            existing.IsActive = dto.IsActive;

            await _eventRepository.UpdateAsync(existing);
            await _eventRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _eventRepository.DeleteAsync(id);
            await _eventRepository.SaveChangesAsync();
        }
    }
}
