using System;
using ZupaTechTest.Domain;
using ZupaTechTest.Domain.Repositories;
using ZupaTechTest.UnitTest;

namespace ZupaTechTest.DomainServices
{
    public class BookingRequestService
    {
        private readonly IRequestedSeatValidator _requestedSeatValidator;
        private readonly IContactDetailsValidator _contactDetailsValidator;
        private readonly IBookingRequestRepository _bookingRequestRepository;

        public BookingRequestService(
            IRequestedSeatValidator requestedSeatValidator, 
            IContactDetailsValidator contactDetailsValidator,
           IBookingRequestRepository bookingRequestRepository)
        {
            _requestedSeatValidator = requestedSeatValidator;
            _contactDetailsValidator = contactDetailsValidator;
            _bookingRequestRepository = bookingRequestRepository;
        }

        public bool Execute(BookingRequest bookingRequest)
        {
            if (!_requestedSeatValidator.Validate(bookingRequest)) {
                return false;
            }

            if (!_contactDetailsValidator.Validate(bookingRequest))
            {
                return false;
            }

            _bookingRequestRepository.Add(bookingRequest);


            return true;
        }
    }
}
