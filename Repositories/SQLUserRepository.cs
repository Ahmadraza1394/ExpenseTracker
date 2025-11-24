using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersonalExpenseTracker.Data;
using PersonalExpenseTracker.Helpers;
using PersonalExpenseTracker.Models.Domain;
using PersonalExpenseTracker.Models.DTO;

namespace PersonalExpenseTracker.Repositories
{
    public class SQLUserRepository : IUserRepository
    {
        private readonly PersonalExpenseDbContext dbContext;
        
        private readonly PasswordHelper passwordHelper;
        //private readonly PasswordHasher<User> passwordHasher = new PasswordHasher<User>();


        public SQLUserRepository(PersonalExpenseDbContext dbContext)
        {
            this.dbContext = dbContext;
         
            this.passwordHelper = new PasswordHelper();
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto dto)
        {
            // Check for existing email
            var existingUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (existingUser != null)
                throw new Exception("Email already exists");

            var hashedPassword = passwordHelper.HashPassword(dto.Password);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Email = dto.Email,
                Password = hashedPassword,
                CreatedAt = DateTime.UtcNow
            };

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Expenses = new List<ExpenseDto>()
            };
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var users = await dbContext.Users
                .Include(u => u.Expenses)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Expenses = u.Expenses.Select(e => new ExpenseDto
                    {
                        Id = e.Id,
                        Title = e.Title,
                        Amount = e.Amount,
                        Date = e.Date,
                        Notes = e.Notes,
                        UserId = u.Id,
                        UserName = u.Name
                    }).ToList()
                })
                .ToListAsync();

            return users;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }



        //Updating user details
        //public async Task<User?> UpdateUser(Guid id, UpdateUserDto dto)
        //{
        //    var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

        //    if (user == null)
        //        return null;

        //    // Update name + email
        //    user.Name = dto.Name;
        //    user.Email = dto.Email;

        //    // Update password only if provided
        //    if (!string.IsNullOrWhiteSpace(dto.Password))
        //    {
        //        user.PasswordHash = passwordHasher.HashPassword(user, dto.Password);
        //    }

        //    await dbContext.SaveChangesAsync();
        //    return user;
        //}

    }
}
