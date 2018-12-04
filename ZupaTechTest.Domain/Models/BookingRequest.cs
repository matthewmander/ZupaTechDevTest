using System;
using System.Collections.Generic;

namespace ZupaTechTest.Domain
{
    public class BookingRequest
    {
        public Guid SlotId { get; set; }

        public IEnumerable<SeatRequest> SeatRequests { get; set; }
    }
}
