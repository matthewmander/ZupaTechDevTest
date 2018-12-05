using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;
using ZupaTechTest.Domain;
using ZupaTechTest.Domain.Validators;

namespace ZupaTechTest.UnitTest
{
    public class SeatRequestValidatorTest
    {
        [Fact]
        public void WhenSeatNumberIsNotSpecifiedThenRequestIsRejected()
        {
            //Given
            var request = new BookingRequest
            {
                SeatRequests = new List<SeatRequest>
                {
                    new SeatRequest{ SeatNumber =""},
                    new SeatRequest{ SeatNumber ="A1"}
                }
            };

            //When
            var sut = new RequestedSeatValidator();
            var result = sut.Validate(request);

            //Then
            result.Success.Should().BeFalse("bacause the seat is not specified");
        }

        [Fact]
        public void WhenTheSameSeatIsRequestedTwiceInTheSameRequestThenItIsRejected()
        {
            //Given
            var request = new BookingRequest
            {
                SeatRequests = new List<SeatRequest>
                {
                    new SeatRequest{ SeatNumber ="A1"},
                    new SeatRequest{ SeatNumber ="A1"}
                }
            };
         
            //When
            var sut = new RequestedSeatValidator();
            var result = sut.Validate(request);

            //Then
            result.Success.Should().BeFalse("bacause the same seat is requested multiple times in the request");
        }

        [Fact]
        public void WhenUniqueSeatsAreRequestedThenValidationPasses()
        {
            //Given
            var request = new BookingRequest
            {
                SeatRequests = new List<SeatRequest>
                {
                    new SeatRequest{ SeatNumber ="A1"},
                    new SeatRequest{ SeatNumber ="A2"}
                }
            };

            //When
            var sut = new RequestedSeatValidator();
            var result = sut.Validate(request);

            //Then
            result.Success.Should().BeTrue("bacause unique seats are requested");
        }

        [Fact]
        public void WhenMoreThenFourSeatsAreRequestedThenValidationFails()
        {
            //Given
            var request = new BookingRequest
            {
                SeatRequests = new List<SeatRequest>
                {
                    new SeatRequest{ SeatNumber ="A1"},
                    new SeatRequest{ SeatNumber ="A2"},
                    new SeatRequest{ SeatNumber ="A3"},
                    new SeatRequest{ SeatNumber ="A4"},
                    new SeatRequest{ SeatNumber ="A5"}
                }
            };

            //When
            var sut = new RequestedSeatValidator();
            var result = sut.Validate(request);

            //Then
            result.Success.Should().BeFalse("bacause too many (>4) seats are requested");
        }

      
    }
}
