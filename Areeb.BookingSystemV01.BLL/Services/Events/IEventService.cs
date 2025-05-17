using Areeb.BookingSystemV01.BLL.Models.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Areeb.BookingSystemV01.BLL.Services.Events
{
    public interface IEventService
    {
        Task<IEnumerable<EventReadDto>> GetAllAsync();
        Task<EventReadDto?> GetByIdAsync(int id);
        Task AddAsync(EventCreateDto eventDto);
        Task UpdateAsync(int id, EventUpdateDto eventDto);
        Task DeleteAsync(int id);
    }
}
