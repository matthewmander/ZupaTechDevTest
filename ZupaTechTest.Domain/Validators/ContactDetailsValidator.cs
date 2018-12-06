using System;
using System.Collections.Generic;
using System.Linq;
using ZupaTechTest.Domain.Repositories;

namespace ZupaTechTest.Domain.Validators
{
    public class ContactDetailsValidator : IContactDetailsValidator
    {
        private readonly ISeatBookingRepository _seatBookingRepository;
        public ContactDetailsValidator(ISeatBookingRepository seatBookingRepository)
        {
            _seatBookingRepository = seatBookingRepository;
        }
        public ValidationResponse Validate(BookingRequest request)
        {
            var validationErrors = new List<string>();
            bool validationSuceeded = true;

            if (request.SeatRequests.Any(x => String.IsNullOrWhiteSpace(x.Name)))
            {
                validationSuceeded = false;
                validationErrors.Add($"All request must have a specified name");
            }
            if (request.SeatRequests.Any(x => String.IsNullOrWhiteSpace(x.Email)))
            {
                validationSuceeded = false;
                validationErrors.Add($"All request must have a specified email address");
            }
            if (request.SeatRequests.GroupBy(x => x.Name).Max(x => x.Count()) > 1)
            {
                validationSuceeded = false;
                validationErrors.Add($"The same name cannot be used for multiple seats");
            }
            if (request.SeatRequests.GroupBy(x => x.Email).Max(x => x.Count()) > 1)
            {
                validationSuceeded = false;
                validationErrors.Add($"The same email address cannot be used for multiple seats");
            }

            var existingRequests = _seatBookingRepository.GetSeatsBySlotId(request.SlotId);
            foreach (var seatRequest in request.SeatRequests)
            {

                if (existingRequests.Any(x => x.Email == seatRequest.Email))
                {
                    validationSuceeded = false;
                    validationErrors.Add($"Email address already used to book a seat: '{seatRequest.Email}'");
                }
                if (existingRequests.Any(x => x.Name == seatRequest.Name))
                {
                    validationSuceeded = false;
                    validationErrors.Add($"Name already used to book a seat: '{seatRequest.Name}'");
                }
            }

            return new ValidationResponse(validationSuceeded, validationErrors);
        }
    }
}