using System;
using Microsoft.Extensions.DependencyInjection;
using Database.Implementations;
using Domain.Contracts;
using Business;
using Business.Config.Validation;

namespace Infraestructure
{
    public static class IoC
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IValidator, Validator>();
        }
    }
}
