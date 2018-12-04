using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ZupaTechTest.Domain;
using ZupaTechTest.Domain.Repositories;
using ZupaTechTest.DomainServices;

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
                .Returns(true);

            var mockContactDetailsValidator = new Mock<IContactDetailsValidator>();
            mockContactDetailsValidator
                .Setup(x => x.Validate(It.IsAny<BookingRequest>()))
                .Returns(true);

            var mockBookingRequestRepo = new Mock<IBookingRequestRepository>();

            var request = new BookingRequest();

            var sut = new BookingRequestService(mockSeatValidator.Object, mockContactDetailsValidator.Object, mockBookingRequestRepo.Object);

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
                .Returns(false);

            var mockContactDetailsValidator = new Mock<IContactDetailsValidator>();
            mockContactDetailsValidator
                .Setup(x => x.Validate(It.IsAny<BookingRequest>()))
                .Returns(true);

            var request = new BookingRequest();

            var sut = new BookingRequestService(mockSeatValidator.Object, mockContactDetailsValidator.Object, null);

            var result = sut.Execute(request);

            result.Should().BeFalse("because validation errors were triggered");
        }

        [Fact]
        public void WhenContactValidationFailsThenResponseIndicatesFailure()
        {
            var mockSeatValidator = new Mock<IRequestedSeatValidator>();
            mockSeatValidator
                .Setup(x => x.Validate(It.IsAny<BookingRequest>()))
                .Returns(true);

            var mockContactDetailsValidator = new Mock<IContactDetailsValidator>();
            mockContactDetailsValidator
                .Setup(x => x.Validate(It.IsAny<BookingRequest>()))
                .Returns(false);

            var request = new BookingRequest();

            var sut = new BookingRequestService(mockSeatValidator.Object, mockContactDetailsValidator.Object, null);

            var result = sut.Execute(request);

            result.Should().BeFalse("because contact detail validation errors were triggered");
        }

        [Fact]
        public void WhenValidationSucceedsThenRepoAddIsCalled()
        {
            var mockSeatValidator = new Mock<IRequestedSeatValidator>();
            mockSeatValidator
                .Setup(x => x.Validate(It.IsAny<BookingRequest>()))
                .Returns(true);

            var mockContactDetailsValidator = new Mock<IContactDetailsValidator>();
            mockContactDetailsValidator
                .Setup(x => x.Validate(It.IsAny<BookingRequest>()))
                .Returns(true);

            var mockBookingRequestRepo = new Mock<IBookingRequestRepository>(); 

            var request = new BookingRequest();

            var sut = new BookingRequestService(mockSeatValidator.Object, 
                mockContactDetailsValidator.Object,
                mockBookingRequestRepo.Object);

            var result = sut.Execute(request);

            mockBookingRequestRepo.Verify(x => x.Add(It.IsAny<BookingRequest>())
                ,"Repository was not called to save booking");
        }
    }


}
