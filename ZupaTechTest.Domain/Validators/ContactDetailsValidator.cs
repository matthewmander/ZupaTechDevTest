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

            var allHaveName = request.SeatRequests.All(x => !String.IsNullOrWhiteSpace(x.Name));
            var allHaveEmail = request.SeatRequests.All(x => !String.IsNullOrWhiteSpace(x.Email));
            var allNameAreUnique = request.SeatRequests.GroupBy(x => x.Name).Max(x => x.Count()) < 2;
            var allEmailsAreUnique = request.SeatRequests.GroupBy(x => x.Email).Max(x => x.Count()) < 2;

            validationSuceeded = allHaveName && allHaveEmail && allNameAreUnique && allEmailsAreUnique;

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

            return new ValidationResponse( validationSuceeded,  validationErrors );
        }
    }
}