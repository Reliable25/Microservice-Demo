using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Newtonsoft.Json;
using TransactionService.Application.Handlers;

namespace TransactionService.Infrastructure.RabbitMQ
{
    public class UserEventConsumer
    {
        public UserEventConsumer(IModel channel, UserLoggedInHandler handler)
        {
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += async (model, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                var data = JsonConvert.DeserializeObject<dynamic>(message);
                int userId = data.UserId;
                await handler.Handle(userId);
            };
            // autoAck: true means Even if processing fails, consider this message successful.That means no retry possible.
            channel.BasicConsume(queue: "TransactionQueue", autoAck: true, consumer: consumer);
        }
    }
}