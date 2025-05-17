using Areeb.BookingSystem.DAL.Entities.Bookings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Areeb.BookingSystemV01.DAL.Persistences.Data.Configurations.Bookings
{
    internal class BookingsConfigurations : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.BookingDate)
                .HasDefaultValueSql("GETDATE()");
            builder.Property(b => b.TicketQuantity)
                .IsRequired()
                .HasDefaultValue(1);
            builder.Property(b => b.TotalPrice)
                .HasColumnType("decimal(18,2)");
            // Configure relationships
            builder.HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(b => b.Event)
                .WithMany(e => e.Bookings)
                .HasForeignKey(b => b.EventId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
   
}
