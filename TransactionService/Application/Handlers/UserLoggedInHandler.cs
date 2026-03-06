using TransactionService.Application.Interfaces;
using TransactionService.Domain.Entities;

namespace TransactionService.Application.Handlers
{
    public class UserLoggedInHandler
    {
        private readonly ITransactionRepository _repository;

        public UserLoggedInHandler(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(int userId)
        {
            var transaction = new Transaction
            {
                UserId = userId,
                Action = "Login",
                Timestamp = DateTime.UtcNow
            };
            Console.WriteLine($"Transaction saved for user {userId}");
            return await _repository.AddAsync(transaction);
        }
    }
}