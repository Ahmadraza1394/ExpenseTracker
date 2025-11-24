using System;
using System.Collections.Generic;

namespace PersonalExpenseTracker.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }   // changed from int → Guid
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation
        public ICollection<Expense> Expenses { get; set; }
    }
}
