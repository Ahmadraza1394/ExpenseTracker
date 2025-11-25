using PersonalExpenseTracker.Models.Domain;

namespace PersonalExpenseTracker.Repositories
{
    public interface IAuthRepository
    {
        Task<User?> ValidateUserAsync(string email, string password);
        string GenerateJwtToken(User user);
    }
}
