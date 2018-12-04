using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ZupaTechTest.IntegrationTests
{
   
    public class NoMoreThanFourSeatsCanBeBooked
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void WhenUpToFourSeatsAreRequestedThenRequestIsAccepted(int noOfSeats)
        {

        }

        [Fact]
        public void WhenMoreThenFourSeatsAreRequestedThenRequestIsRejected()
        {

        }
    }
}
