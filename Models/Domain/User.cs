using System;
using System.Collections.Generic;

namespace PersonalExpenseTracker.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        //  Store hashed password
        public string PasswordHash { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public ICollection<Expense> Expenses { get; set; }
    }
}
