using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PersonalExpenseTracker.Data;
using PersonalExpenseTracker.Models.Domain;
using PersonalExpenseTracker.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class AuthRepository : IAuthRepository
{
    private readonly PersonalExpenseDbContext db;
    private readonly IConfiguration config;
    private readonly PasswordHasher<User> passwordHasher = new PasswordHasher<User>();

    public AuthRepository(PersonalExpenseDbContext db, IConfiguration config)
    {
        this.db = db;
        this.config = config;
    }

    public async Task<User?> ValidateUserAsync(string email, string password)
    {
        var user = await db.Users.FirstOrDefaultAsync(x => x.Email == email);

        if (user == null)
            return null;

        var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

        return result == PasswordVerificationResult.Success ? user : null;
    }

    public string GenerateJwtToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim("userId", user.Id.ToString()),
            new Claim("email", user.Email),
            new Claim(ClaimTypes.Name, user.Name)
        };

        var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(int.Parse(config["Jwt:ExpiryMinutes"])),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
