using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PersonManager.RabbitMQ.Abstract;
using RabbitMQ.Client;
using System.Text;

namespace PersonManager.RabbitMQ.Concreate
{
    public class RabbitMqPublisher : IRabbitMqPublisher
    {
        private readonly IConfiguration _configuration;

        public RabbitMqPublisher(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Publish<T>(T message)
        {

            try
            {
                var rabbitMqUrl = _configuration["RabbitMq:Url"];
                var queue = _configuration["RabbitMq:Queue"];
                var factory = new ConnectionFactory()
                {
                    Uri = new Uri(rabbitMqUrl)
                };

                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();

                channel.QueueDeclare(queue: queue, durable: true, exclusive: false, autoDelete: false, arguments: null);

                var json = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(exchange: "", routingKey: queue, body: body);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
    }
}
