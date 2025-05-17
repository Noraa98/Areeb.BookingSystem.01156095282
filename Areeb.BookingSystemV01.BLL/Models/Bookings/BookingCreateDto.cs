using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Areeb.BookingSystemV01.BLL.Models.Bookings
{
    public class BookingCreateDto
    {
        public int UserId { get; set; }
        public int EventId { get; set; }
        public DateTime BookingDate { get; set; }

        public int TicketQuantity { get; set; }
    }
}
