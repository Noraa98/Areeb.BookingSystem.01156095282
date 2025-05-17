using Areeb.BookingSystem.DAL.Entities.Bookings;
using Areeb.BookingSystemV01.BLL.Models.Bookings;
using Areeb.BookingSystemV01.DAL.Persistences.Repositories.Bookings;
using Areeb.BookingSystemV01.DAL.Persistences.Repositories.Events;
using Areeb.BookingSystemV01.DAL.Persistences.Repositories.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Areeb.BookingSystemV01.BLL.Services.Bookings
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IUserRepository _userRepository;

        public BookingService(IBookingRepository bookingRepository,
                              IEventRepository eventRepository,
                              IUserRepository userRepository)
        {
            _bookingRepository = bookingRepository;
            _eventRepository = eventRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<BookingDto>> GetAllAsync()
        {
            var bookings = await _bookingRepository.GetAllAsync();

            return bookings.Select(b => new BookingDto
            {
                Id = b.Id,
                TicketQuantity = b.TicketQuantity,
                TotalPrice = b.TotalPrice,
                BookingDate = b.BookingDate,
                UserFullName = $"{b.User?.FirstName} {b.User?.LastName}",
                EventName = b.Event?.EventName
            });
        }

        public async Task<BookingDto?> GetByIdAsync(int id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);
            if (booking == null) return null;

            return new BookingDto
            {
                Id = booking.Id,
                TicketQuantity = booking.TicketQuantity,
                TotalPrice = booking.TotalPrice,
                BookingDate = booking.BookingDate,
                UserFullName = $"{booking.User?.FirstName} {booking.User?.LastName}",
                EventName = booking.Event?.EventName
            };
        }

        public async Task<IEnumerable<BookingDto>> GetByUserIdAsync(int userId)
        {
            var bookings = await _bookingRepository.GetByUserIdAsync(userId);

            return bookings.Select(b => new BookingDto
            {
                Id = b.Id,
                TicketQuantity = b.TicketQuantity,
                TotalPrice = b.TotalPrice,
                BookingDate = b.BookingDate,
                EventName = b.Event?.EventName,
                UserFullName = $"{b.User?.FirstName} {b.User?.LastName}"
            });
        }

        public async Task<bool> HasUserBookedEventAsync(int userId, int eventId)
        {
            return await _bookingRepository.HasUserBookedEventAsync(userId, eventId);
        }

        public async Task AddAsync(BookingCreateDto bookingDto)
        {
            var eventEntity = await _eventRepository.GetByIdAsync(bookingDto.EventId);
            if (eventEntity == null)
                throw new Exception("Event not found");

            var totalPrice = eventEntity.Price * bookingDto.TicketQuantity;

            var booking = new Booking
            {
                UserId = bookingDto.UserId,
                EventId = bookingDto.EventId,
                TicketQuantity = bookingDto.TicketQuantity,
                TotalPrice = totalPrice,
                BookingDate = DateTime.Now
            };

            await _bookingRepository.AddAsync(booking);
            await _bookingRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _bookingRepository.DeleteAsync(id);
            await _bookingRepository.SaveChangesAsync();
        }

        
    }

}
