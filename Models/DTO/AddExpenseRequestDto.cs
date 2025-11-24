using PersonalExpenseTracker.Models.Domain;

namespace PersonalExpenseTracker.Models.DTO
{
    public class AddExpenseRequestDto
    {
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string? Notes { get; set; }

        // Foreign Key
        public Guid UserId { get; set; }

       
    }
}
