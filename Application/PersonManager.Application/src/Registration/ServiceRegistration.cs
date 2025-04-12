using Microsoft.Extensions.DependencyInjection;
using PersonManager.Application.Abstractions.Person;
using PersonManager.Application.Abstractions.PersonInfo;
using PersonManager.Application.Mappings;
using PersonManager.Application.Person;
using PersonManager.Application.PersonInfo;

namespace PersonManager.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IPersonInfoService, PersonInfoService>();
            services.AddAutoMapper(typeof(MappingProfile));

            return services;
        }
    }
}
