using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public static class MyServiceCollectionExtensions
    {
        public static IServiceCollection AddMyServiceDependencies(this IServiceCollection services, string connectionString)
        {
            services.AddEntityFrameworkSqlServer()
            .AddDbContext<PartyRoomContext>((serviceProvider, options) =>
                options.UseSqlServer(connectionString)
                .UseInternalServiceProvider(serviceProvider)
                   );
             return services;
        }
    }

}