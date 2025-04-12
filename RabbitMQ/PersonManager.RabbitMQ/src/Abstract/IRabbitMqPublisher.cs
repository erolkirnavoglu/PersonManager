namespace PersonManager.RabbitMQ.Abstract
{
    public interface IRabbitMqPublisher
    {
       public void Publish<T>(T message);
    }
}
