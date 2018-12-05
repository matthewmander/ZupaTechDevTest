using System;
using System.Collections.Generic;

namespace ZupaTechTest.Domain
{
    public class BookingRequest
    {
        public BookingRequest()
        {
            SeatRequests = new List<SeatRequest>();
        }
        public Guid SlotId { get; set; }

        public IEnumerable<SeatRequest> SeatRequests { get; set; }
    }
}
