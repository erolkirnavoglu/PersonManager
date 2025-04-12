using Microsoft.Extensions.DependencyInjection;
using PersonManager.RabbitMQ.BackGroundService;

namespace PersonManager.RabbitMQ.Registration
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddRabbitMqServices(this IServiceCollection services)
        {
            services.AddHostedService<ReportBackgroundService>();
            return services;
        }
    }
}
