using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZupaTechTest.Domain;

namespace ZupaTechTest.Models
{
    public class SeatBookingRequest
    {
        public Guid SlotId { get; set; }
        public IEnumerable<SeatRequest> SeatRequests { get; set; }

        public BookingRequest ToDomainObject()
        {
            return new BookingRequest
            {
                SlotId = SlotId,
                SeatRequests = SeatRequests.Select(x=> new Domain.SeatRequest
                {
                    Email = x.Email,
                    Name = x.Name, 
                    SeatNumber = x.SeatNumber
                })
            };
        }
    }

}
