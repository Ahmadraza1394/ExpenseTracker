using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PersonalExpenseTracker.Data;
using PersonalExpenseTracker.Models.Domain;
using PersonalExpenseTracker.Models.DTO;

public class SQLExpenseRepository : IExpenseRepository
{
    private readonly PersonalExpenseDbContext dbContext;
    private readonly IMapper mapper;

    public SQLExpenseRepository(PersonalExpenseDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<List<ExpenseDto>> GetAllAsync()
    {
        var expenses = await dbContext.Expenses.Include(e => e.User).ToListAsync();
        return mapper.Map<List<ExpenseDto>>(expenses);
    }

    public async Task<ExpenseDto> CreateAsync(AddExpenseRequestDto dto)
    {
        var expense = mapper.Map<Expense>(dto);
        await dbContext.Expenses.AddAsync(expense);
        await dbContext.SaveChangesAsync();

        // Load related User for mapping UserName
        await dbContext.Entry(expense).Reference(e => e.User).LoadAsync();

        return mapper.Map<ExpenseDto>(expense);
    }

    public async Task<ExpenseDto> GetByIdAsync(Guid id)
    {
        var expense = await dbContext.Expenses.Include(e => e.User)
                             .FirstOrDefaultAsync(e => e.Id == id);
        return mapper.Map<ExpenseDto>(expense);
    }


    public async Task<ExpenseDto> UpdateAsync(Guid id, AddExpenseRequestDto dto)
    {
        var existingExpense = await dbContext.Expenses.FindAsync(id);
        if (existingExpense == null) return null;

        mapper.Map(dto, existingExpense);
        await dbContext.SaveChangesAsync();

        await dbContext.Entry(existingExpense).Reference(e => e.User).LoadAsync();
        return mapper.Map<ExpenseDto>(existingExpense);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var expense = await dbContext.Expenses.FindAsync(id);
        if (expense == null) return false;

        dbContext.Expenses.Remove(expense);
        await dbContext.SaveChangesAsync();
        return true;
    }
    public async Task<List<ExpenseDto>> GetAllByUserAsync(Guid userId)
    {
        var expenses = await dbContext.Expenses
            .Where(x => x.UserId == userId)
            .Include(x => x.User)
            .ToListAsync();

        return mapper.Map<List<ExpenseDto>>(expenses);
    }

}
