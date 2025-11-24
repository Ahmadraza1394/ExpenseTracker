namespace PersonalExpenseTracker.Models.DTO
{
    public class CreateUserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } // plain password, will be hashed
    }
}
