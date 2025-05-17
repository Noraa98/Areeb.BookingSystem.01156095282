using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Areeb.BookingSystemV01.BLL.Models.Events
{
    public class EventCreateDto
    {
        public string? EventName { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? Location { get; set; }
        public DateTime EventDate { get; set; }
        public string? Venue { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
    }
}
