using System;
using System.Collections.Generic;
using System.Text;

namespace ZupaTechTest.Domain.Repositories
{
    public interface ISeatBookingRepository
    {
        void Add(SeatRequest seatRequest, Guid slotId);
    }
}
