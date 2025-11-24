using Microsoft.AspNetCore.Mvc;
using PersonalExpenseTracker.Models.DTO;
using PersonalExpenseTracker.Repositories;

namespace PersonalExpenseTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await userRepository.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
        {
            try
            {
                var user = await userRepository.CreateUserAsync(dto);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("by-email")]
        public async Task<IActionResult> GetUserByEmail([FromQuery] string email)
        {
            var user = await userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        //updating user
        //[HttpPut("{id:guid}")]
        //public async Task<IActionResult> UpdateUser(Guid id, UpdateUserDto dto)
        //{
        //    var updatedUser = await userRepository.UpdateUser(id, dto);

        //    if (updatedUser == null)
        //        return NotFound("User not found.");

        //    return Ok(updatedUser);
        //}

    }
}
