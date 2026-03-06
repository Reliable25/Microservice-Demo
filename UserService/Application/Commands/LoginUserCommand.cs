using MediatR;
namespace UserService.Application.Commands
{

    public class LoginUserCommand : IRequest<Unit>
    {
        public int UserId { get; set; }
    }
}
