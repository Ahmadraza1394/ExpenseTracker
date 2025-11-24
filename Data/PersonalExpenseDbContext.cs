using Microsoft.EntityFrameworkCore;
using PersonalExpenseTracker.Models.Domain;

namespace PersonalExpenseTracker.Data
{
    public class PersonalExpenseDbContext : DbContext
    {
        public PersonalExpenseDbContext(DbContextOptions<PersonalExpenseDbContext> options)
            : base(options)
        {
        }

        // Domain DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<Expense> Expenses { get; set; }

        // Seed Data (Optional)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ---- Seed Users ----
            var users = new List<User>
            {
                new User
                {
                    Id = 1,
                    Name = "Test User",
                    Email = "test@example.com",
                    Password = "hashedpassword123", // store hashed in real use
                    CreatedAt = DateTime.UtcNow
                }
            };

            modelBuilder.Entity<User>().HasData(users);

            // ---- Seed Expenses ----
            var expenses = new List<Expense>
            {
                new Expense
                {
                    Id = 1,
                    Title = "Grocery Shopping",
                    Amount = 1500.50M,
                    Date = DateTime.UtcNow.AddDays(-2),
                    Notes = "Bought vegetables and snacks",
                    UserId = 1
                },
                new Expense
                {
                    Id = 2,
                    Title = "Mobile Recharge",
                    Amount = 500,
                    Date = DateTime.UtcNow.AddDays(-1),
                    Notes = "Jazz monthly bundle",
                    UserId = 1
                }
            };

            modelBuilder.Entity<Expense>().HasData(expenses);
        }
    }
}
