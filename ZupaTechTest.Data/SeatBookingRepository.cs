using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZupaTechTest.Domain;
using ZupaTechTest.Domain.Repositories;

namespace ZupaTechTest.Data
{
    public class SeatBookingRepository : ISeatBookingRepository
    {
        private BookingContext _bookingContext;
        public SeatBookingRepository(BookingContext bookingContext)
        {
            _bookingContext = bookingContext;
        }

        public void Add(SeatRequest seatRequest, Guid slotId)
        {
            var meetingSlot = _bookingContext.MeetingSlots.SingleOrDefault(x => x.MeetingSlotId == slotId);
            var booking = new SeatBooking
            {
                Email = seatRequest.Email,
                Name = seatRequest.Name,
                SeatBookingId = Guid.NewGuid(),
                MeetingSlot = meetingSlot,
                SeatNumber = seatRequest.SeatNumber
            };

            _bookingContext.SeatBookings.Add(booking);
            _bookingContext.SaveChanges();
        }
    }
}
