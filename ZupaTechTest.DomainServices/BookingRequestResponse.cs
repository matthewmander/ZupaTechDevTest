using System;
using System.Collections.Generic;
using System.Text;

namespace ZupaTechTest.DomainServices
{
    public class BookingRequestResponse
    {
        public bool Success { get; set; }
        public IEnumerable<string> ValdationErrors {get; set;}
    }
}
