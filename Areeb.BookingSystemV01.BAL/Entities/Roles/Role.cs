using Areeb.BookingSystem.DAL.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Areeb.BookingSystem.DAL.Entities.Roles
{
    public class Role : IdentityRole
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? Description { get; set; }

        // Navigation property
        public ICollection<User>? Users { get; set; }
        
    }
}
