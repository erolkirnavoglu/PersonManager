using Microsoft.Extensions.DependencyInjection;
using PersonManager.Application.Abstractions.Person;
using PersonManager.Application.Mappings;
using PersonManager.Application.Person;

namespace PersonManager.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IPersonService, PersonService>();
            services.AddAutoMapper(typeof(MappingProfile));

            return services;
        }
    }
}
