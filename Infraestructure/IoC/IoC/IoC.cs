using System;
using Microsoft.Extensions.DependencyInjection;
using DatabaseImplementation;
using DatabaseInterface;
using Business;

namespace Infraestructure
{
    public static class IoC
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IEventRepository, EventRepository>();
        }
    }
}
