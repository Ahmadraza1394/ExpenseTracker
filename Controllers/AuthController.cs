using Microsoft.AspNetCore.Mvc;
using PersonalExpenseTracker.Models.DTO;
using PersonalExpenseTracker.Repositories;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthRepository authRepo;

    public AuthController(IAuthRepository authRepo)
    {
        this.authRepo = authRepo;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var user = await authRepo.ValidateUserAsync(dto.Email, dto.Password);

        if (user == null)
            return Unauthorized("Invalid email or password");

        var token = authRepo.GenerateJwtToken(user);

        return Ok(new
        {
            token = token,
            userId = user.Id,
            userName = user.Name,
            email = user.Email
        });
    }
}
