using System;
using System.Linq;

namespace ZupaTechTest.Domain
{
    public class ContactDetailsValidator : IContactDetailsValidator
    {
        public ContactDetailsValidator()
        {

        }
        public bool Validate(BookingRequest request)
        {
            var allHaveName = request.SeatRequests.All(x => !String.IsNullOrWhiteSpace(x.Name));
            var allHaveEmail = request.SeatRequests.All(x => !String.IsNullOrWhiteSpace(x.Email));
            var allNameAreUnique = request.SeatRequests.GroupBy(x => x.Name).Max(x => x.Count()) < 2;
            var allEmailsAreUnique = request.SeatRequests.GroupBy(x => x.Email).Max(x => x.Count()) < 2;
            return allHaveName && allNameAreUnique && allHaveEmail && allEmailsAreUnique;
        }
    }
}