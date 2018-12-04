using System;
using System.Collections.Generic;
using System.Text;

namespace ZupaTechTest.Domain.Repositories
{
    public interface IBookingRequestRepository
    {
        void Add(BookingRequest bookingRequest);
    }
}
