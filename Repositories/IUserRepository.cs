using PersonalExpenseTracker.Models.Domain;
using PersonalExpenseTracker.Models.DTO;

namespace PersonalExpenseTracker.Repositories
{
    public interface IUserRepository
    {
        Task<UserDto> CreateUserAsync(CreateUserDto dto);
        Task<List<UserDto>> GetAllUsersAsync();
        Task<User?> GetByEmailAsync(string email); // optional, for login or email checks

        

        Task<UserDto?> UpdateUserAsync(Guid id, UpdateUserDto dto);

        Task<UserDto?> DeleteUserAsync(Guid id);

    }
}
