﻿using System;
using ZupaTechTest.Domain;
using ZupaTechTest.Domain.Repositories;
using ZupaTechTest.Domain.Validators;

namespace ZupaTechTest.Domain.Services
{
    public class BookingRequestService : IBookingRequestService
    {
        private readonly IRequestedSeatValidator _requestedSeatValidator;
        private readonly IContactDetailsValidator _contactDetailsValidator;
        private readonly ISeatBookingRepository _seatBookingRepository;

        public BookingRequestService(
            IRequestedSeatValidator requestedSeatValidator,
            IContactDetailsValidator contactDetailsValidator,
            ISeatBookingRepository seatBookingRepository)
        {
            _requestedSeatValidator = requestedSeatValidator;
            _contactDetailsValidator = contactDetailsValidator;
            _seatBookingRepository = seatBookingRepository;
        }

        public BookingRequestResponse Execute(BookingRequest bookingRequest)
        {
            var seatRequestsAreValid = _requestedSeatValidator.Validate(bookingRequest);
            if (!seatRequestsAreValid.Success)
            {
                return new BookingRequestResponse
                {
                    Success = false,
                    ValdationErrors = seatRequestsAreValid.ValidationErrors
                };
            }

            var contactDetailsAreValid = _contactDetailsValidator.Validate(bookingRequest);
            if (!contactDetailsAreValid.Success)
            {
                return new BookingRequestResponse
                {
                    Success = false,
                    ValdationErrors = contactDetailsAreValid.ValidationErrors
                };
            }

            foreach (var seatRequest in bookingRequest.SeatRequests)
            {
                _seatBookingRepository.Add(seatRequest, bookingRequest.SlotId);
            }

            return new BookingRequestResponse
            {
                Success = true
            };
        }
    }
}
