namespace TransactionService.Domain.Entities
{
    // Domain/Entities/Transaction.cs
    public class Transaction
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Action { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
