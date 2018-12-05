using ZupaTechTest.Domain;

namespace ZupaTechTest.Domain.Services
{
    public interface IBookingRequestService
    {
        bool Execute(BookingRequest bookingRequest);
    }
}