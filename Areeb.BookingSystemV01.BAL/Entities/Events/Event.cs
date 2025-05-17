using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Areeb.BookingSystem.DAL.Entities.Bookings;

namespace Areeb.BookingSystem.DAL.Entities.Events
{
    public class Event
    {
        public int Id { get; set; }
        public string? EventName { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? Location { get; set; }
        public DateTime EventDate { get; set; }
        public string? Venue { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public ICollection<Booking>? Bookings { get; set; }
    }

}
