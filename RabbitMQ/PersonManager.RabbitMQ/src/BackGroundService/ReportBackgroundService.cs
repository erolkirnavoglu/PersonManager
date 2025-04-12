
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using PersonManager.RabbitMQ.Concreate;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using PersonManager.Application.Abstractions.ReportDetail;


namespace PersonManager.RabbitMQ.BackGroundService
{
    public class ReportBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IConfiguration _configuration;
        private IConnection _connection;
        private IModel _channel;
        public ReportBackgroundService(IServiceScopeFactory scopeFactory, IConfiguration configuration)
        {
            _scopeFactory = scopeFactory;
            _configuration = configuration;
        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri(_configuration["RabbitMq:Url"]),
                DispatchConsumersAsync = true
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(
                queue: _configuration["RabbitMq:Queue"],
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            return base.StartAsync(cancellationToken);
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                var consumer = new AsyncEventingBasicConsumer(_channel);
                consumer.Received += async (model, ea) =>
                {

                    var body = ea.Body.ToArray();
                    var messageString = Encoding.UTF8.GetString(body);
                    var message = JsonConvert.DeserializeObject<ReportRequestMessage>(messageString);

                    using var scope = _scopeFactory.CreateScope();
                    var reportService = scope.ServiceProvider.GetRequiredService<IReportDetailService>();

                    await reportService.CreateAsync(message.ReportId);

                    _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);

                };

                _channel.BasicConsume(
                queue: _configuration["RabbitMq:Queue"],
                autoAck: false,
                consumer: consumer);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

            return Task.CompletedTask;
        }
        public override void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
            base.Dispose();
        }
    }

}
