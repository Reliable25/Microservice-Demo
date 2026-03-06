using TransactionService.Application.Interfaces;
using TransactionService.Domain.Entities;
using TransactionService.Infrastructure.Db;
using Microsoft.EntityFrameworkCore;

namespace TransactionService.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IDbContextFactory<TransactionDbContext> _dbContextFactory;

        public TransactionRepository(IDbContextFactory<TransactionDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<bool> AddAsync(Transaction transaction)
        {
            // Create a new DbContext from the factory
            await using var context = _dbContextFactory.CreateDbContext();
            context.Transactions.Add(transaction);
            return await context.SaveChangesAsync() > 0 ? true : false;
        }

        // Optional: a method to get all transactions (for controller)
        public async Task<List<Transaction>> GetAllAsync()
        {
            await using var context = _dbContextFactory.CreateDbContext();
            return await context.Transactions.ToListAsync();
        }
    }
}