using Confluent.Kafka;

namespace TransactionService.Infrastructure.Kafka
{
    public class KafkaConsumerService : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "transaction-service",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            consumer.Subscribe("user-events");

            while (!stoppingToken.IsCancellationRequested)
            {
                var result = consumer.Consume(stoppingToken);
                Console.WriteLine(result.Message.Value);
            }

            return Task.CompletedTask;
        }
    }
}
