using System;
using System.Collections.Generic;

namespace PersonalExpenseTracker.Models.DTO
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<ExpenseDto> Expenses { get; set; } = new List<ExpenseDto>();
    }
}
