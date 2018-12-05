using System;
using System.Collections.Generic;
using System.Text;

namespace ZupaTechTest.Domain.Services
{
    public class BookingRequestResponse
    {
        public BookingRequestResponse()
        {
            ValdationErrors = new List<string>();
        }
        public bool Success { get; set; }
        public IEnumerable<string> ValdationErrors {get; set;}
    }
}
