using ZupaTechTest.Domain.Validators;

namespace ZupaTechTest.Domain.Validators
{
    public interface IContactDetailsValidator
    {
        ValidationResponse Validate(BookingRequest request);
    }
}