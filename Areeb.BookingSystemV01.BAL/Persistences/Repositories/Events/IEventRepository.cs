using Areeb.BookingSystem.DAL.Entities.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Areeb.BookingSystemV01.DAL.Persistences.Repositories.Events
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAllAsync();
        Task<Event> GetByIdAsync(int id);
        Task AddAsync(Event @event);
        Task UpdateAsync(Event @event);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}
