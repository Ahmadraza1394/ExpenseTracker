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

        public DbSet<User> Users { get; set; }
        public DbSet<Expense> Expenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ---------- FIXED GUIDS ----------
            var user1Id = Guid.Parse("c6b7c56f-1c72-4c2e-9c11-5cdd7d00c111");
            var user2Id = Guid.Parse("52a6240b-0f04-4be8-b54a-d1f6c6e05522");

            var expense1Id = Guid.Parse("f94c2c07-9f92-4c21-9cb8-df1e0227d333");
            var expense2Id = Guid.Parse("ab7bb8f4-950e-4c84-b6d7-214a96ac3333");
            var expense3Id = Guid.Parse("d39afe32-a4f7-4d53-9668-0f0726c44444");

            // ---------- USERS SEED ----------
            modelBuilder.Entity<User>().HasData(new List<User>
            {
                new User
                {
                    Id = user1Id,
                    Name = "Ahmad Raza",
                    Email = "ahmad@example.com",
                    PasswordHash = "123456", // normal demo password (should be hashed in real apps)
                    CreatedAt = DateTime.UtcNow
                },
                new User
                {
                    Id = user2Id,
                    Name = "Ali Khan",
                    Email = "ali@example.com",
                    PasswordHash = "password123",
                    CreatedAt = DateTime.UtcNow
                }
            });

            // ---------- EXPENSES SEED ----------
            modelBuilder.Entity<Expense>().HasData(new List<Expense>
            {
                new Expense
                {
                    Id = expense1Id,
                    Title = "Grocery Shopping",
                    Amount = 2500.75M,
                    Date = DateTime.UtcNow.AddDays(-3),
                    Notes = "Bought fruits, vegetables, snacks",
                    UserId = user1Id
                },
                new Expense
                {
                    Id = expense2Id,
                    Title = "Fuel Refill",
                    Amount = 5200.00M,
                    Date = DateTime.UtcNow.AddDays(-1),
                    Notes = "Car petrol full tank",
                    UserId = user1Id
                },
                new Expense
                {
                    Id = expense3Id,
                    Title = "Internet Bill",
                    Amount = 2500.00M,
                    Date = DateTime.UtcNow.AddDays(-5),
                    Notes = "PTCL monthly bill",
                    UserId = user2Id
                }
            });
        }
    }
}
