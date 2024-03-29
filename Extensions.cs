﻿using System;
using Microsoft.Extensions.DependencyInjection;
using Zoho.Interfaces;
using Zoho.Services;

// ReSharper disable SwitchStatementHandlesSomeKnownEnumValuesWithDefault
// ReSharper disable once CheckNamespace
namespace Zoho
{
    public static class Extensions
    {
        public static IServiceCollection AddZohoServices(this IServiceCollection services, Action<Options> configureOptions, ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            services.AddHttpClient<ZohoService>("ZohoService");
            services.AddSingleton<Factory>().Configure(configureOptions);

            switch (lifetime)
            {
                case ServiceLifetime.Singleton:
                    services.AddSingleton<IBookService, BookService>().Configure(configureOptions);
                    services.AddSingleton<IBookingService, BookingService>().Configure(configureOptions);
                    services.AddSingleton<ICampaignService, CampaignService>().Configure(configureOptions);
                    services.AddSingleton<ISubscriptionService, SubscriptionService>().Configure(configureOptions);
                    services.AddSingleton<ICrmService, CrmService>().Configure(configureOptions);
                    services.AddSingleton<IProjectService, ProjectService>().Configure(configureOptions);
                    services.AddSingleton<ZohoWrapper>();
                    break;
                case ServiceLifetime.Scoped:
                    services.AddScoped<IBookService, BookService>().Configure(configureOptions);
                    services.AddScoped<IBookingService, BookingService>().Configure(configureOptions);
                    services.AddScoped<ICampaignService, CampaignService>().Configure(configureOptions);
                    services.AddScoped<ISubscriptionService, SubscriptionService>().Configure(configureOptions);
                    services.AddScoped<ICrmService, CrmService>().Configure(configureOptions);
                    services.AddScoped<IProjectService, ProjectService>().Configure(configureOptions);
                    services.AddScoped<ZohoWrapper>();
                    break;
                case ServiceLifetime.Transient:
                    services.AddTransient<IBookService, BookService>().Configure(configureOptions);
                    services.AddTransient<IBookingService, BookingService>().Configure(configureOptions);
                    services.AddTransient<ICampaignService, CampaignService>().Configure(configureOptions);
                    services.AddTransient<ISubscriptionService, SubscriptionService>().Configure(configureOptions);
                    services.AddTransient<ICrmService, CrmService>().Configure(configureOptions);
                    services.AddTransient<IProjectService, ProjectService>().Configure(configureOptions);
                    services.AddTransient<ZohoWrapper>();
                    break;
            }


            return services;
        }
    }
}
