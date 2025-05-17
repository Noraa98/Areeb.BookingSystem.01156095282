using Areeb.BookingSystemV01.BLL.Services.Bookings;
using Areeb.BookingSystemV01.BLL.Services.Events;
using Areeb.BookingSystemV01.BLL.Services.Roles;
using Areeb.BookingSystemV01.BLL.Services.Users;
using Areeb.BookingSystemV01.DAL.Persistences.Data;
using Areeb.BookingSystemV01.DAL.Persistences.Repositories.Bookings;
using Areeb.BookingSystemV01.DAL.Persistences.Repositories.Events;
using Areeb.BookingSystemV01.DAL.Persistences.Repositories.Roles;
using Areeb.BookingSystemV01.DAL.Persistences.Repositories.Users;
using Microsoft.EntityFrameworkCore;
namespace Areeb.BookingSystemV01.DAL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationDbContext>((options) =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IBookingRepository, BookingRepository>();
            builder.Services.AddScoped<IEventRepository, EventRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();


            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<IBookingService, BookingService>();
            builder.Services.AddScoped<IEventService, EventService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IRoleService, RoleService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

           

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
