using ZupaTechTest.Domain;

namespace ZupaTechTest.UnitTest
{
    public interface IRequestedSeatValidator
    {
        bool Validate(BookingRequest request);
    }
}