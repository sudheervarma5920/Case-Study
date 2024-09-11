using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TwitterAPI.Entities;

namespace TwitterAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TwitterContext _context;
        private readonly IConfiguration _configuration;
        public UserRepository(TwitterContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task Register(User user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new Exception("An error occured while registering the user.", ex);
            }
            
        }

        public async Task<User> ValidUser(string email, string password)
        {
            try
            {
                return await _context.Users.SingleOrDefaultAsync(u => u.Email == email && u.Password == password);
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new Exception("An error occured while validating the user.",ex);
            }
            
        }
        public async Task Update(User user)
        {
            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new Exception("An error occurred while updating the user.",ex);
            }
            
        }

        public async Task Delete(string id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    throw new Exception("User not found");
                }
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new Exception("An error occurred while deleting the user.", ex);
            }
            
            
        }

        public async Task<List<User>> GetAllUsers()
        {
            {
                try
                {
                    return await _context.Users.ToListAsync();
                }
                catch (Exception ex)
                {
                    // Log exception here
                    throw new Exception("An error occurred while retrieving the tweets.", ex);
                }
            }
        }

        public async Task<User> GetUserById(string userId)
        {

            try
            {
                var user = await _context.Users.SingleOrDefaultAsync(u => u.UserId == userId);
                return user;
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new Exception("An error occurred while retrieving the tweets by UserId.", ex);
            }
        }
    }
}
