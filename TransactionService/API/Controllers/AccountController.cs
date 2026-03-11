using Microsoft.AspNetCore.Mvc;
using TransactionService.Application.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly ITransactionRepository _repository;

    public AccountController(ITransactionRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(new
        {
            Account = "account"
        });
    }
}