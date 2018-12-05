using ZupaTechTest.Domain;
using ZupaTechTest.Domain.Validators;

namespace ZupaTechTest.Domain.Validators
{
    public interface IRequestedSeatValidator
    {
        ValidationResponse Validate(BookingRequest request);
    }
}