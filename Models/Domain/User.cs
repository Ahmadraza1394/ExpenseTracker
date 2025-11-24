namespace PersonalExpenseTracker.Models.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } // store hashed
        public DateTime CreatedAt { get; set; }

        // Navigation
        public ICollection<Expense> Expenses { get; set; }
    }
}
