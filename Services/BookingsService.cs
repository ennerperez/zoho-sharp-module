using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Zoho.Interfaces;
using Newtonsoft.Json.Serialization;

// ReSharper disable once CheckNamespace
namespace Zoho.Services
{
    public class BookingService : IBookingService
    {
        private string Name => Enum.GetName(Enums.Module.Booking);
        
        private readonly Factory _factory;

        public BookingService(Factory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _factory.SerializerSettings = new JsonSerializerSettings()
            {
                ContractResolver = new DefaultContractResolver()
            };
        }
        
        public async Task<T> GetBooking<T>(string bookingId)
        {
            //GET 'https://www.zohoapis.com/bookings/v1/json/getappointment?booking_id=#AN-00014'
            var client = await _factory.CreateAsync();
            var response = await client.InvokeGetAsync<T>(Name, $"getappointment?booking_id={bookingId}","response");
            return response;
        }

        public async Task<string> GetOption(string key)
        {
            var client = await _factory.CreateAsync();
            return client.GetOption(Name, key);
        }
    }
}
