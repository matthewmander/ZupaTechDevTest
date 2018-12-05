using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZupaTechTest.Domain.Services;
using ZupaTechTest.Models;

namespace ZupaTechTest.Controllers
{
    [Route("api/[controller]")]
    public class SeatBookingController : Controller
    {
        private IBookingRequestService _bookingRequestService;
        public SeatBookingController(IBookingRequestService bookingRequestService)
        {
            _bookingRequestService = bookingRequestService;
        }
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]SeatBookingRequest request)
        {
            _bookingRequestService.Execute(request.ToDomainObject());
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


    }
}
