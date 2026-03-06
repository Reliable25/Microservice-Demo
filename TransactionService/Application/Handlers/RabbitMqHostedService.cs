using RabbitMQ.Client;
using TransactionService.Application.Handlers;
using TransactionService.Infrastructure.RabbitMQ;

public class RabbitMqHostedService : IHostedService
{
    private IConnection _connection;
    private IModel _channel;
    private readonly IServiceProvider _serviceProvider;

    public RabbitMqHostedService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        // 1️⃣ Direct queue approach
        _channel.QueueDeclare(queue: "TransactionQueue",
                              durable: true,
                              exclusive: false,
                              autoDelete: false,
                              arguments: null);

        using var scope = _serviceProvider.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<UserLoggedInHandler>();

        // Start the consumer
        var consumer = new UserEventConsumer(_channel, handler);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _channel?.Close();
        _connection?.Close();
        return Task.CompletedTask;
    }
}