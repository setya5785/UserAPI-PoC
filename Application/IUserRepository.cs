using UserAPI.Domain;

namespace UserAPI.Application
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsersAsync(string UserId);
        Task<(int, string)> SetUserAsync(User user);
        Task<(int, string)> DeleteUserAsync(int userId);
    }
}
