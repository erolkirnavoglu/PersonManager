using Microsoft.Extensions.DependencyInjection;
using PersonManager.Application.Abstractions.Person;
using PersonManager.Application.Abstractions.PersonInfo;
using PersonManager.Application.Abstractions.Report;
using PersonManager.Application.Abstractions.ReportDetail;
using PersonManager.Application.Mappings;
using PersonManager.Application.Person;
using PersonManager.Application.PersonInfo;
using PersonManager.Application.Report;
using PersonManager.Application.ReportDetail;
using PersonManager.RabbitMQ.Abstract;
using PersonManager.RabbitMQ.Concreate;

namespace PersonManager.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IPersonInfoService, PersonInfoService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IReportDetailService, ReportDetailService>();
            services.AddSingleton<IRabbitMqPublisher, RabbitMqPublisher>();
            services.AddAutoMapper(typeof(MappingProfile));
            
            return services;
        }
    }
}
