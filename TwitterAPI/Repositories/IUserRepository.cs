using TwitterAPI.Entities;

namespace TwitterAPI.Repositories
{
    public interface IUserRepository
    {
        Task  Register(User user);
        Task<User> ValidUser(string email, string password);
        Task Update(User user);
        Task<User> GetUserById(string userId);
        Task<List<User>> GetAllUsers();
        Task Delete(string id);
    }
}
