using Microsoft.EntityFrameworkCore;
using PersonalExpenseTracker.Data;
using PersonalExpenseTracker.Models.Domain;

namespace PersonalExpenseTracker.Repositories
{
    public class SQLExpenseRepository : IExpenseRepository
    {
        private readonly PersonalExpenseDbContext dbContext;

        public SQLExpenseRepository(PersonalExpenseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public Task<Expense> CreateAsync(Expense expense)
        {
            throw new NotImplementedException();
        }

        public Task<Expense> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

      

        public Task<Expense> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Expense> UpdateAsync(int id, Expense expense)
        {
            throw new NotImplementedException();
        }

        public async Task <List<Expense>> GetAllAsync()
        {
           var expenses= await dbContext.Expenses.ToListAsync();
            return expenses;
        }
    }
}
