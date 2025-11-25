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
                PasswordHash = hashedPassword,
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

        public async Task<UserDto?> DeleteUserAsync(Guid id)
        {
            // Fetch user with expenses (optional: to return full dto before deletion)
            var user = await dbContext.Users
                .Include(u => u.Expenses)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return null;

            // Prepare a DTO to return before deleting
            var deletedUserDto = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Expenses = user.Expenses.Select(e => new ExpenseDto
                {
                    Id = e.Id,
                    Title = e.Title,
                    Amount = e.Amount,
                    Date = e.Date,
                    Notes = e.Notes,
                    UserId = user.Id,
                    UserName = user.Name
                }).ToList()
            };

            // Remove user (EF will delete expenses if cascade delete is enabled)
            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync();

            return deletedUserDto;
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


        public async Task<UserDto?> UpdateUserAsync(Guid id, UpdateUserDto dto)
        {
            //  Fetch the user, include expenses to avoid null errors
            var user = await dbContext.Users
                .Include(u => u.Expenses)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null) return null;

            // Update the basic fields
            user.Name = dto.Name;
            user.Email = dto.Email;

            // Update password only if provided
            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                user.PasswordHash = passwordHelper.HashPassword(dto.Password);
            }

            await dbContext.SaveChangesAsync();

            // Map to UserDto
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Expenses = (user.Expenses ?? new List<Expense>()) // prevent null
                    .Select(e => new ExpenseDto
                    {
                        Id = e.Id,
                        Title = e.Title,
                        Amount = e.Amount,
                        Date = e.Date,
                        Notes = e.Notes,
                        UserId = user.Id,
                        UserName = user.Name
                    }).ToList()
            };
        }


    }
}
