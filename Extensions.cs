using System;
using Infrastructure.Enterprise.Interfaces;
using Infrastructure.Enterprise.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Enterprise
{
    public static class Extensions
    {
        public static IServiceCollection AddEnterpriseServices(this IServiceCollection services, Action<Options> configureOptions = null)
        {
            services.AddSingleton<ISubscriptionService, SubscriptionService>().Configure(configureOptions);
            services.AddSingleton<ICampaignService, CampaignService>().Configure(configureOptions);
            services.AddSingleton<IBookService, BookService>().Configure(configureOptions);

            return services;
        }
    }
}