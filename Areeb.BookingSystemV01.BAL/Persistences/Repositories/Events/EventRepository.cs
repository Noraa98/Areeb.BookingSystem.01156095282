using Areeb.BookingSystem.DAL.Entities.Events;
using Areeb.BookingSystemV01.DAL.Persistences.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Areeb.BookingSystemV01.DAL.Persistences.Repositories.Events
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _context;

        public EventRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            return await _context.Events
                .Include(e => e.Bookings)
                    .ThenInclude(b => b.User)
                .ToListAsync();
        }

        public async Task<Event?> GetByIdAsync(int id)
        {
            return await _context.Events
                .Include(e => e.Bookings)
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task AddAsync(Event @event)
        {
            await _context.Events.AddAsync(@event);
        }

        public async Task UpdateAsync(Event @event)
        {
            _context.Events.Update(@event);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            if (@event is not null)
            {
                _context.Events.Remove(@event);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}