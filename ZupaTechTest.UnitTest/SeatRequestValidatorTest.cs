using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;
using ZupaTechTest.Domain;

namespace ZupaTechTest.UnitTest
{
    public class SeatRequestValidatorTest
    {
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
            result.Should().BeFalse("bacause the same seat is requested multiple times in the request");
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
            result.Should().BeTrue("bacause unique seats are requested");
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
            result.Should().BeFalse("bacause too many (>4) seats are requested");
        }

        [Fact]
        public void WhenNameIsNotProvidedThenValidationFails()
        {
            //Given
            var request = new BookingRequest
            {
                SeatRequests = new List<SeatRequest>
                {
                    new SeatRequest{ SeatNumber ="A1", Name ="" },
                }
            };

            //When
            var sut = new ContactDetailsValidator();
            var result = sut.Validate(request);

            //Then
            result.Should().BeFalse("bacause name is not provided");
        }

        [Fact]
        public void WhenEmailIsNotProvidedThenValidationFails()
        {
            //Given
            var request = new BookingRequest
            {
                SeatRequests = new List<SeatRequest>
                {
                    new SeatRequest{ SeatNumber ="A1", Name ="Test Name", Email = "" },
                }
            };

            //When
            var sut = new ContactDetailsValidator();
            var result = sut.Validate(request);

            //Then
            result.Should().BeFalse("bacause email address is not provided");
        }

        [Fact]
        public void WhenNameAndEmailAreProvidedThenValidationPasses()
        {
            //Given
            var request = new BookingRequest
            {
                SeatRequests = new List<SeatRequest>
                {
                    new SeatRequest{ SeatNumber ="A1", Name ="TestName", Email = "a@b.co.uk" },
                }
            };

            //When
            var sut = new ContactDetailsValidator();
            var result = sut.Validate(request);

            //Then
            result.Should().BeTrue("bacause name is provided");
        }

        [Fact]
        public void WhenNameIsNotUniqueThenValidationFails()
        {
            //Given
            var request = new BookingRequest
            {
                SeatRequests = new List<SeatRequest>
                {
                    new SeatRequest{ SeatNumber ="", Name ="TestName", Email="Email1" },
                    new SeatRequest{ SeatNumber ="", Name ="TestName", Email="Email2" },
                }
            };

            //When
            var sut = new ContactDetailsValidator();
            var result = sut.Validate(request);

            //Then
            result.Should().BeFalse("bacause name is used multiple times in request");
        }

        [Fact]
        public void WhenEmailIsNotUniqueThenValidationFails()
        {
            //Given
            var request = new BookingRequest
            {
                SeatRequests = new List<SeatRequest>
                {
                    new SeatRequest{ SeatNumber ="", Name ="TestName", Email="Email" },
                    new SeatRequest{ SeatNumber ="", Name ="TestName2", Email="Email" },
                }
            };

            //When
            var sut = new ContactDetailsValidator();
            var result = sut.Validate(request);

            //Then
            result.Should().BeFalse("bacause name is used multiple times in request");
        }

        [Fact]
        public void WhenNamesAndEmailsAreUniqueThenValidationPassses()
        {
            //Given
            var request = new BookingRequest
            {
                SeatRequests = new List<SeatRequest>
                {
                    new SeatRequest{ SeatNumber ="", Name ="TestName", Email="Email1" },
                    new SeatRequest{ SeatNumber ="", Name ="TestName2", Email= "Email2" },
                }
            };

            //When
            var sut = new ContactDetailsValidator();
            var result = sut.Validate(request);

            //Then
            result.Should().BeTrue("bacause names are unique");
        }
    }
}
