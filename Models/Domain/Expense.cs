using System;

namespace PersonalExpenseTracker.Models.Domain
{
    public class Expense
    {
        public Guid Id { get; set; }        // changed from int → Guid
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string? Notes { get; set; }

        // Foreign Key
        public Guid UserId { get; set; }    // changed from int → Guid

        // Navigation
        public User User { get; set; }
    }
}
