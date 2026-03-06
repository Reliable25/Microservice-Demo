using MediatR;
using UserService.Application.Commands;
using UserService.Application.Interfaces;

namespace UserService.Application.Handlers
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Unit>
    {
        private readonly IUserEventPublisher _publisher;

        public LoginUserCommandHandler(IUserEventPublisher publisher)
        {
            _publisher = publisher;
        }

        public async Task<Unit> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            await _publisher.PublishUserLoggedInEvent(request.UserId);
            return Unit.Value;
        }
    }
}