using ZupaTechTest.Domain;
using ZupaTechTest.Domain.Validators;

namespace ZupaTechTest.Domain.Services
{
    public interface IBookingRequestService
    {
        BookingRequestResponse Execute(BookingRequest bookingRequest);
    }
}