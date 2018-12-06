using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]SeatBookingRequest request)
        {
            if (request == null) return BadRequest();

            var result = _bookingRequestService.Execute(request.ToDomainObject());
            if (!result.Success)
            {
                return BadRequest(result.ValdationErrors);
            }
            return Ok();
        }

    }
}
