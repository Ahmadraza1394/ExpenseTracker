namespace PersonalExpenseTracker.Models.DTO
{
    public class UpdateUserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }

        // Optional — only update if provided
        public string? Password { get; set; }
    }

}
