using Areeb.BookingSystem.DAL.Entities.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Areeb.BookingSystemV01.DAL.Persistences.Data.Configurations.Events
{
    internal class EventsConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.EventName)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(e => e.EventDate)
                .IsRequired();
            builder.Property(e => e.Location)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(e => e.Price)
                .HasColumnType("decimal(18,2)");
            // Configure relationships
            builder.HasMany(e => e.Bookings)
                .WithOne(b => b.Event)
                .HasForeignKey(b => b.EventId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
   
}
