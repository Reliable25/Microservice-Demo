using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Commands;

[ApiController]
[Route("api/[Controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login/{userId}")]
    public async Task<IActionResult> Login(int userId)
    {
        var command = new LoginUserCommand
        {
            UserId = userId
        };

        await _mediator.Send(command);

        return Ok("User logged in");
    }
}