using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using UserService.Application.Interfaces;

namespace UserService.Infrastructure.RabbitMQ
{
    public class UserEventPublisher : IUserEventPublisher
    {
        private readonly IModel _channel;

        public UserEventPublisher(IModel channel)
        {
            _channel = channel;

            // ensure queue exists
            _channel.QueueDeclare(queue: "TransactionQueue",
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
        }

        public Task PublishUserLoggedInEvent(int userId)
        {
            var message = new { UserId = userId, Event = "UserLoggedIn" };
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            _channel.BasicPublish(
                exchange: "",                // default exchange
                routingKey: "TransactionQueue", // must match queue in TransactionService
                basicProperties: null,
                body: body);

            Console.WriteLine("Message published to TransactionQueue");
            return Task.CompletedTask;
        }
    }
}