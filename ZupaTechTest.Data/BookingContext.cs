using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ZupaTechTest.Data
{
    public class BookingContext : DbContext
    {
        public BookingContext(DbContextOptions<BookingContext> options) : base(options)
        {
            
        }
        public DbSet<MeetingSlot> MeetingSlots { get; set; }
        public DbSet<SeatBooking> SeatBookings { get; set; }
    }

    public class MeetingSlot
    {
        public Guid MeetingSlotId { get; set; }
        public ICollection<SeatBooking> SeatBookings { get; set; }
    }

    public class SeatBooking
    {
        public Guid SeatBookingId { get; set; }
        public string SeatNumber { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public MeetingSlot MeetingSlot { get; set; }
    }
}
