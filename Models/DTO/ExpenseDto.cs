namespace PersonalExpenseTracker.Models.DTO
{
    public class ExpenseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }

        public Guid UserId { get; set; }
        public string UserName { get; set; }  // Only return needed user data
    }

}
