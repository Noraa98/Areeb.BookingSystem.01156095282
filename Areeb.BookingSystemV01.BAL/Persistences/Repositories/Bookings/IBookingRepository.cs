using Areeb.BookingSystem.DAL.Entities.Bookings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Areeb.BookingSystemV01.DAL.Persistences.Repositories.Bookings
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetAllAsync();
        Task<IEnumerable<Booking>> GetByUserIdAsync(int userId);
        Task<bool> HasUserBookedEventAsync(int userId, int eventId);
        Task<Booking> GetByIdAsync(int id);
        Task AddAsync(Booking booking);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}
