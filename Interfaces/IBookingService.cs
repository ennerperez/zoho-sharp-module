using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace Zoho.Interfaces
{
    /// <summary>
    ///     https://www.zoho.com/books/api/v3/
    /// </summary>
    public interface IBookingService : IZohoService
    {
        Task<T> GetBooking<T>(string bookingId);
    }
}
