using MediatR;
using UserService.Application.Commands;
using UserService.Application.Interfaces;
using UserService.Infrastructure.Kafka;

namespace UserService.Application.Handlers
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Unit>
    {
        private readonly IUserEventPublisher _rabbitPublisher;
        private readonly UserEventProducer _kafkaProducer;

        public LoginUserCommandHandler(
            IUserEventPublisher rabbitPublisher,
            UserEventProducer kafkaProducer)
        {
            _rabbitPublisher = rabbitPublisher;
            _kafkaProducer = kafkaProducer;
        }
        public async Task<Unit> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            await _rabbitPublisher.PublishUserLoggedInEvent(request.UserId);

            await _kafkaProducer.PublishAsync($"User {request.UserId} logged in");

            return Unit.Value;
        }
    }
}