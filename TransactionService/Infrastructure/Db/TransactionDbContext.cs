using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TransactionService.Domain.Entities;
namespace TransactionService.Infrastructure.Db
{
    public class TransactionDbContext : DbContext
    {
        public TransactionDbContext(DbContextOptions<TransactionDbContext> options)
            : base(options)
        {
        }

        public DbSet<Transaction> Transactions { get; set; }
    }
}
