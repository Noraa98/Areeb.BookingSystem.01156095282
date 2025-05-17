using Areeb.BookingSystemV01.BLL.Models.Bookings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Areeb.BookingSystemV01.BLL.Services.Bookings
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingDto>> GetAllAsync();
        Task<BookingDto?> GetByIdAsync(int id);
        Task<IEnumerable<BookingDto>> GetByUserIdAsync(int userId);
        Task<bool> HasUserBookedEventAsync(int userId, int eventId);
        Task AddAsync(BookingCreateDto bookingDto);
        Task DeleteAsync(int id);
    }
}
