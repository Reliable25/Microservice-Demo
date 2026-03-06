using TransactionService.Domain.Entities;
namespace TransactionService.Application.Interfaces
{
    public interface ITransactionRepository
    {
        Task<bool> AddAsync(Transaction transaction);
        Task<List<Transaction>> GetAllAsync();
    }
}
