using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Areeb.BookingSystemV01.BLL.Models.Bookings
{
    public class BookingDto
    {
        public int Id { get; set; }
        public int TicketQuantity { get; set; }

        public decimal TotalPrice { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime EventDate { get; set; }

        public string? UserFullName { get; set; }
        public string? EventName { get; set; }
        

    }
}
