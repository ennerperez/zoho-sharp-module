using System;
using Zoho.Interfaces;
using Zoho.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Zoho
{
    public static class Extensions
    {
        public static IServiceCollection AddZohoServices(this IServiceCollection services, Action<Options> configureOptions = null)
        {
            services.AddSingleton<ISubscriptionService, SubscriptionService>().Configure(configureOptions);
            services.AddSingleton<ICampaignService, CampaignService>().Configure(configureOptions);
            services.AddSingleton<IBookService, BookService>().Configure(configureOptions);

            return services;
        }
    }
}