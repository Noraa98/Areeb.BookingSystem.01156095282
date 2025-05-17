using Areeb.BookingSystem.DAL.Entities.Bookings;
using Areeb.BookingSystem.DAL.Entities.Events;
using Areeb.BookingSystem.DAL.Entities.Roles;
using Areeb.BookingSystem.DAL.Entities.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Areeb.BookingSystemV01.DAL.Persistences.Data
{
    public static class ApplicationDbContextSeed
    {
        public static void Seed(ApplicationDbContext dbcontext, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            // 1. Seed Roles
            if (!roleManager.Roles.Any())
            {
                var roles = new List<Role>
                {
                    new Role { Name = "Admin", NormalizedName = "ADMIN", Description = "Administrator role" },
                    new Role { Name = "User", NormalizedName = "USER", Description = "Standard user role" }
                };

                foreach (var role in roles)
                {
                    var result = roleManager.CreateAsync(role).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception("Failed to create role: " + role.Name);
                    }
                }
            }

            // 2. Seed Admin User
            if (!userManager.Users.Any())
            {
                var adminRole = roleManager.Roles.FirstOrDefault(r => r.Name == "Admin");
                if (adminRole == null) throw new Exception("Admin role not found.");

                var adminUser = new User
                {
                    UserName = "admin@example.com",
                    Email = "admin@example.com",
                    FirstName = "Admin",
                    LastName = "User",
                    RoleId = adminRole.Id,
                    EmailConfirmed = true,
                };

                var result = userManager.CreateAsync(adminUser, "Admin@123").Result;
                if (!result.Succeeded)
                {
                    throw new Exception("Failed to create admin user.");
                }

                var addRoleResult = userManager.AddToRoleAsync(adminUser, "Admin").Result;
                if (!addRoleResult.Succeeded)
                {
                    throw new Exception("Failed to assign admin role to admin user.");
                }
            }

            // 3. Seed Events
            if (!dbcontext.Events.Any())
            {
                var events = new List<Event>
                {
                    new Event
                    {
                        EventName = "Tech Conference 2025",
                        Description = "A conference about the latest in tech.",
                        Category = "Technology",
                        EventDate = DateTime.Now.AddMonths(1),
                        Venue = "Convention Center",
                        Price = 100,
                        ImageUrl = "techconf.jpg"
                    },
                    new Event
                    {
                        EventName = "Music Festival",
                        Description = "A festival with live music from top artists.",
                        Category = "Music",
                        EventDate = DateTime.Now.AddMonths(2),
                        Venue = "City Park",
                        Price = 50,
                        ImageUrl = "musicfest.jpg"
                    }
                };

                dbcontext.Events.AddRange(events);
                dbcontext.SaveChanges();
            }

            // 4. Optionally, seed Bookings if needed
            if (!dbcontext.Bookings.Any())
            {
                var user = userManager.Users.FirstOrDefault();
                var firstEvent = dbcontext.Events.FirstOrDefault();
                if(user != null && firstEvent != null)
                {
                    var booking = new Booking
                    {
                        UserId = user.Id,
                        EventId = firstEvent.Id,
                        BookingDate = DateTime.Now
                    };
                    dbcontext.Bookings.Add(booking);
                    dbcontext.SaveChanges();
                }
            }
        }
    }
}
