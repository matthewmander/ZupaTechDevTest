using ZupaTechTest.Domain;

namespace ZupaTechTest.DomainServices
{
    public interface IBookingRequestService
    {
        BookingRequestResponse Execute(BookingRequest bookingRequest);
    }
}