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

    [HttpGet]
    public async Task<IActionResult> GetAllExpense()
    {
        var expenses = await expenseRepository.GetAllAsync();
        return Ok(expenses);
    }
    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetExpenseById([FromRoute] Guid id)
    {
        var expense = await expenseRepository.GetByIdAsync(id);
        if (expense == null)
        {
            return NotFound();
        }
        return Ok(expense);
    }

    //Deleting expense
    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> DeleteExpense([FromRoute] Guid id)
    {
        var deleted = await expenseRepository.DeleteAsync(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }


    //Updating expense
    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> UpdateExpense([FromRoute] Guid id, [FromBody] AddExpenseRequestDto dto)
    {
        var updatedExpense = await expenseRepository.UpdateAsync(id, dto);
        if (updatedExpense == null)
        {
            return NotFound();
        }
        return Ok(updatedExpense);
    }

    [HttpPost]
    public async Task<IActionResult> AddExpense([FromBody] AddExpenseRequestDto dto)
    {
        var expenseDto = await expenseRepository.CreateAsync(dto);
        return Ok(expenseDto);
    }
}
