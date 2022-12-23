using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Zoho.Services;

// ReSharper disable once CheckNamespace
namespace Zoho
{
    public class Factory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Options _options;

        public Factory(IServiceProvider serviceProvider, IOptionsMonitor<Options> optionsMonitor)
        {
            _serviceProvider = serviceProvider;
            _options = optionsMonitor.CurrentValue;
        }

        public async Task<ZohoService> CreateAsync()
        {
            var client = _serviceProvider.GetRequiredService<ZohoService>();
            client.Configure(_options);
            if (string.IsNullOrEmpty(ZohoService.AuthToken))
            {
                await client.GetTokenAsync();
            }

            return client;
        }
    }
}
