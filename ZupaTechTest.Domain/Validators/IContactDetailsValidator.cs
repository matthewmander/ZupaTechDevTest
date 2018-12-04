namespace ZupaTechTest.Domain
{
    public interface IContactDetailsValidator
    {
        bool Validate(BookingRequest request);
    }
}