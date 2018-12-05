using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using ZupaTechTest.Domain;
using ZupaTechTest.Domain.Repositories;
using ZupaTechTest.Domain.Services;
using ZupaTechTest.Domain.Validators;

namespace ZupaTechTest.UnitTest.DomainServices
{
    public class BookingRequestServiceTest
    {
        [Fact]
        public void ValidatorsAreCalled()
        {
            var mockSeatValidator = new Mock<IRequestedSeatValidator>();
            mockSeatValidator
                .Setup(x => x.Validate(It.IsAny<BookingRequest>()))
                .Returns(new ValidationResponse(true));

            var mockContactDetailsValidator = new Mock<IContactDetailsValidator>();
            mockContactDetailsValidator
                .Setup(x => x.Validate(It.IsAny<BookingRequest>()))
                .Returns(new ValidationResponse(true));

            var mockBookingRequestRepo = new Mock<ISeatBookingRepository>();

            var request = new BookingRequest();

            var sut = new BookingRequestService(
                mockSeatValidator.Object, 
                mockContactDetailsValidator.Object, 
                mockBookingRequestRepo.Object);

            sut.Execute(request);

            mockSeatValidator.Verify(x => x.Validate(request), "Expected the seat validator to be called");
            mockContactDetailsValidator.Verify(x => x.Validate(request), "Expected the contact details validator to be called");
        }

        [Fact]
        public void WhenValidationFailsThenResponseIndicatesFailure()
        {
            var mockSeatValidator = new Mock<IRequestedSeatValidator>();
            mockSeatValidator
                .Setup(x => x.Validate(It.IsAny<BookingRequest>()))
                .Returns(new ValidationResponse(false));

            var mockContactDetailsValidator = new Mock<IContactDetailsValidator>();
            mockContactDetailsValidator
                .Setup(x => x.Validate(It.IsAny<BookingRequest>()))
                .Returns(new ValidationResponse(true));

            var request = new BookingRequest();

            var sut = new BookingRequestService(mockSeatValidator.Object, mockContactDetailsValidator.Object, null);

            var result = sut.Execute(request);

            result.Success.Should().BeFalse("because validation errors were triggered");
        }

        [Fact]
        public void WhenContactValidationFailsThenResponseIndicatesFailure()
        {
            var mockSeatValidator = new Mock<IRequestedSeatValidator>();
            mockSeatValidator
                .Setup(x => x.Validate(It.IsAny<BookingRequest>()))
                .Returns(new ValidationResponse(true));

            var mockContactDetailsValidator = new Mock<IContactDetailsValidator>();
            mockContactDetailsValidator
                .Setup(x => x.Validate(It.IsAny<BookingRequest>()))
                .Returns(new ValidationResponse(false));

            var request = new BookingRequest();

            var sut = new BookingRequestService(mockSeatValidator.Object, mockContactDetailsValidator.Object, null);

            var result = sut.Execute(request);

            result.Success.Should().BeFalse("because contact detail validation errors were triggered");
        }

        [Fact]
        public void WhenValidationSucceedsThenRepoAddIsCalled()
        {
            var mockSeatValidator = new Mock<IRequestedSeatValidator>();
            mockSeatValidator
                .Setup(x => x.Validate(It.IsAny<BookingRequest>()))
                .Returns(new ValidationResponse(true));

            var mockContactDetailsValidator = new Mock<IContactDetailsValidator>();
            mockContactDetailsValidator
                .Setup(x => x.Validate(It.IsAny<BookingRequest>()))
                .Returns(new ValidationResponse(true));

            var mockBookingRequestRepo = new Mock<ISeatBookingRepository>();

            var request = new BookingRequest
            {
                SeatRequests = new List<SeatRequest>
                {
                    new SeatRequest{Email = "a", Name = "b", SeatNumber = "c"}
                }
            };

            var sut = new BookingRequestService(mockSeatValidator.Object,
                mockContactDetailsValidator.Object,
                mockBookingRequestRepo.Object);

            var result = sut.Execute(request);

            mockBookingRequestRepo.Verify(x => x.Add(request.SeatRequests.ToList().First(), request.SlotId)
                , "Repository was not called to save booking");
        }
    }


}
