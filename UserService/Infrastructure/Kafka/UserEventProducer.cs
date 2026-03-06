using Confluent.Kafka;

namespace UserService.Infrastructure.Kafka
{
    public class UserEventProducer
    {
        private readonly IProducer<Null, string> _producer;

        public UserEventProducer()
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            };

            _producer = new ProducerBuilder<Null, string>(config).Build();
        }

        public async Task PublishAsync(string message)
        {
            await _producer.ProduceAsync("user-events",
                new Message<Null, string> { Value = message });
        }
    }
}
