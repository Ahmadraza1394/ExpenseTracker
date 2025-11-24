using Microsoft.AspNetCore.Identity;

namespace PersonalExpenseTracker.Helpers
{
    public class PasswordHelper
    {
        private readonly PasswordHasher<object> hasher = new PasswordHasher<object>();

        // Hash the password
        public string HashPassword(string password)
        {
            return hasher.HashPassword(null, password);
        }

        // Verify password
        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            var result = hasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}
