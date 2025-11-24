using PersonalExpenseTracker.Models.DTO;

public interface IExpenseRepository
{
    Task<List<ExpenseDto>> GetAllAsync();
    Task<ExpenseDto> CreateAsync(AddExpenseRequestDto dto);
    Task<ExpenseDto> GetByIdAsync(Guid id);
    Task<ExpenseDto> UpdateAsync(Guid id, AddExpenseRequestDto dto);
    Task<bool> DeleteAsync(Guid id);
}
