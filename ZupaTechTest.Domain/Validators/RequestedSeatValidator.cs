using System;
using System.Linq;
using ZupaTechTest.Domain;

namespace ZupaTechTest.UnitTest
{
    public class RequestedSeatValidator
    {
        public RequestedSeatValidator()
        {
        }

        public bool Validate(BookingRequest request)
        {
            if (request.SeatRequests.Any(x => String.IsNullOrWhiteSpace(x.SeatNumber))) return false;
            if (request.SeatRequests.Count() > 4) return false;
            return request.SeatRequests.GroupBy(x => x.SeatNumber).Max(x => x.Count()) < 2;
        }
    }
}