using Microsoft.AspNetCore.Mvc;
using TransactionService.Application.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionRepository _repository;

    public TransactionController(ITransactionRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var transactions = await _repository.GetAllAsync();
        return Ok(transactions);
    }
}