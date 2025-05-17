using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Areeb.BookingSystemV01.BLL.Models.Users
{
    public class UserReadDto
    {
        public int Id { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? RoleName { get; set; }
        public DateTime CreatedAt { get; set; }
        public int RoleId { get; set; }
    }
}
