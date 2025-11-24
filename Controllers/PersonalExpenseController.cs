using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalExpenseTracker.Data;
using Microsoft.EntityFrameworkCore;
using PersonalExpenseTracker.Repositories;
using PersonalExpenseTracker.Models.Domain;

namespace PersonalExpenseTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            var domainExpenseModel= await expenseRepository.GetAllAsync();

            



            return Ok(domainExpenseModel);
        }
       

    }
}
