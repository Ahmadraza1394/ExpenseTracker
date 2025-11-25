using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalExpenseTracker.Models.DTO;

[ApiController]
[Route("api/[controller]")]
public class PersonalExpenseController : ControllerBase
{
    private readonly IExpenseRepository expenseRepository;

    public PersonalExpenseController(IExpenseRepository expenseRepository)
    {
        this.expenseRepository = expenseRepository;
    }
 
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAllExpense([FromQuery] int? month, [FromQuery] int? year)
    {
        var userId = User.FindFirst("userId")?.Value;
        if (userId == null) return Unauthorized();

        Guid userGuid = Guid.Parse(userId);

        var expenses = await expenseRepository.GetAllByUserAsync(userGuid);

        // Filter by month and year if provided
        if (month.HasValue && year.HasValue)
        {
            expenses = expenses
                .Where(e => e.Date.Month == month.Value && e.Date.Year == year.Value)
                .ToList();
        }

        return Ok(expenses);
    }





    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetExpenseById([FromRoute] Guid id)
    {
        var userId = User.FindFirst("userId")?.Value;
        if (userId == null)
            return Unauthorized("Invalid token");

        var expense = await expenseRepository.GetByIdAsync(id);
        if (expense == null)
            return NotFound();

        if (expense.UserId != Guid.Parse(userId))
            return Forbid("You cannot access someone else's expense");

        return Ok(expense);
    }


    //Deleting expense
    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteExpense([FromRoute] Guid id)
    {
        //Extract the user ID from JWT token
        var userId = User.FindFirst("userId")?.Value;
        if (userId == null)
            return Unauthorized("Invalid token");

        var expense = await expenseRepository.GetByIdAsync(id);
        if (expense == null)
            return NotFound();

        if (expense.UserId != Guid.Parse(userId))
            return Forbid("You cannot delete someone else's expense");

        var deleted = await expenseRepository.DeleteAsync(id);
        return NoContent();
    }



    //Updating expense
    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateExpense([FromRoute] Guid id, [FromBody] AddExpenseRequestDto dto)
    {
        var userId = User.FindFirst("userId")?.Value;
        if (userId == null)
            return Unauthorized("Invalid token");

        var existingExpense = await expenseRepository.GetByIdAsync(id);
        if (existingExpense == null)
            return NotFound();

        if (existingExpense.UserId != Guid.Parse(userId))
            return Forbid("You cannot update someone else's expense");

        dto.UserId = Guid.Parse(userId);

        var updatedExpense = await expenseRepository.UpdateAsync(id, dto);
        return Ok(updatedExpense);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddExpense([FromBody] AddExpenseRequestDto dto)
    {
        var userId = User.FindFirst("userId")?.Value;
        if (userId == null) return Unauthorized();

        dto.UserId = Guid.Parse(userId);
        var expenseDto = await expenseRepository.CreateAsync(dto);
        return Ok(expenseDto);
    }

}
