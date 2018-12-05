using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;
using ZupaTechTest.Domain;
using ZupaTechTest.Domain.Validators;

namespace ZupaTechTest.UnitTest
{
    public class ContactDetailsValidatorTest
    {

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
            result.Success.Should().BeFalse("bacause name is not provided");
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
            result.Success.Should().BeFalse("bacause email address is not provided");
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
            result.Success.Should().BeTrue("bacause name is provided");
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
            result.Success.Should().BeFalse("bacause name is used multiple times in request");
        }

        [Fact]
        public void WhenEmailIsNotUniqueThenValidationFails()
        {
            //Given
            var request = new BookingRequest
            {
                SeatRequests = new List<SeatRequest>
                {
                    new SeatRequest{ SeatNumber ="A1", Name ="TestName", Email="Email" },
                    new SeatRequest{ SeatNumber ="A2", Name ="TestName2", Email="Email" },
                }
            };

            //When
            var sut = new ContactDetailsValidator();
            var result = sut.Validate(request);

            //Then
            result.Success.Should().BeFalse("bacause name is used multiple times in request");
        }

        [Fact]
        public void WhenNamesAndEmailsAreUniqueThenValidationPassses()
        {
            //Given
            var request = new BookingRequest
            {
                SeatRequests = new List<SeatRequest>
                {
                    new SeatRequest{ SeatNumber ="A1", Name ="TestName", Email="Email1" },
                    new SeatRequest{ SeatNumber ="A2", Name ="TestName2", Email= "Email2" },
                }
            };

            //When
            var sut = new ContactDetailsValidator();
            var result = sut.Validate(request);

            //Then
            result.Success.Should().BeTrue("bacause names are unique");
        }
    }
}
