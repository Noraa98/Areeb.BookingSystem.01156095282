using Areeb.BookingSystem.DAL.Entities.Events;
using Areeb.BookingSystem.DAL.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Areeb.BookingSystem.DAL.Entities.Bookings
{
    public class Booking
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TicketQuantity { get; set; } = 1;
        public decimal TotalPrice { get; set; }
        public int EventId { get; set; }
        public DateTime BookingDate { get; set; } = DateTime.Now;

        // Navigation properties
        public User? User { get; set; }
        public Event? Event { get; set; }
    }
}
