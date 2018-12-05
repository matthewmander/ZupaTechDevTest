using System;
using System.Collections.Generic;
using System.Linq;
using ZupaTechTest.Domain;
using ZupaTechTest.Domain.Repositories;
using ZupaTechTest.Domain.Validators;

namespace ZupaTechTest.Domain.Validators
{
    public class RequestedSeatValidator : IRequestedSeatValidator
    {
        private readonly ISeatBookingRepository _seatBookingRepository;
        public RequestedSeatValidator(ISeatBookingRepository seatBookingRepository )
        {
            _seatBookingRepository = seatBookingRepository;
        }

        public ValidationResponse Validate(BookingRequest request)
        {
            var validationErrors = new List<string>();
            bool validationSuceeded = true;
            if (request.SeatRequests.Any(x => String.IsNullOrWhiteSpace(x.SeatNumber)))
            {
                validationSuceeded = false;
                validationErrors.Add("All seat numbers must be stated");
            }

            if (request.SeatRequests.Count() > 4)
            {
                validationSuceeded = false;
                validationErrors.Add("Only 4 seats can be booked at one time");
            }

            if (request.SeatRequests.GroupBy(x => x.SeatNumber).Max(x => x.Count()) > 1)
            {
                validationSuceeded = false;
                validationErrors.Add("An attempt was made to book the same seat more than once in the same request");
            }

            var existingRequests = _seatBookingRepository.GetSeatsBySlotId(request.SlotId);
            foreach (var seatRequest in request.SeatRequests)
            {
                if (existingRequests.Any(x => x.SeatNumber == seatRequest.SeatNumber))
                {
                    validationSuceeded = false;
                    validationErrors.Add($"Seat already booked {seatRequest.SeatNumber}");
                }
            }
            return new ValidationResponse (validationSuceeded, validationErrors );
        }
    }
}