using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ZupaTechTest.IntegrationTests
{
    
    public class AUniqueNameAndEmailAddressIsRequiredForEachSeat
    {
        [Fact]
        public void WhenNoNameIsGivenThenRequestIsRejected()
        {

        }

        [Fact]
        public void WhenNameAlreadyExistsThenRequestIsRejected()
        {

        }

        [Fact]
        public void WhenNameIsDuplicetedInRequestThenItIsRejected()
        {

        }

        [Fact]
        public void WhenNoEmailAddressIsGivenThenRequestIsRejected()
        {

        }
        [Fact]
        public void WhenEmailAlreadExistsThenRequestIsRejected()
        {

        }
        [Fact]
        public void WhenEmailIsDuplictedInRequestThenRequestIsRejected()
        {

        }
        
    }

}
