using Microsoft.EntityFrameworkCore;
using TwitterAPI.Entities;

namespace TwitterAPI.Repositories
{
    public class FollowRepository : IFollowRepository
    {
        private readonly TwitterContext _context;
        private readonly IConfiguration _configuration;
        public FollowRepository(TwitterContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task AddFollowing(Follow follow)
        {
            try
            {
                await _context.Follows.AddAsync(follow);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here
                throw new Exception("An error occurred while adding the following relationship.", ex);
            }
        }

        public async Task<IEnumerable<User>> GetFollowers(string followingId)
        {
            try
            {
                var followers = await _context.Follows
                                              .Where(f => f.FollowingId == followingId)
                                              .Select(f => f.Follower)
                                              .ToListAsync();
                return followers;
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here
                throw new Exception("An error occurred while retrieving the followers.", ex);
            }
        }

        public async Task<IEnumerable<User>> GetFollowings(string userId)
        {
            try
            {
                var followingIds = await _context.Follows
                                                 .Where(f => f.UserId == userId)
                                                 .Select(f => f.FollowingId)
                                                 .ToListAsync();

                var followings = await _context.Users
                                               .Where(u => followingIds.Contains(u.UserId))
                                               .ToListAsync();

                return followings;
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here
                throw new Exception("An error occurred while retrieving the followings.", ex);
            }
        }

        public async Task RemoveFollowing(string userId, string followingId)
        {
            try
            {
                var follow = await _context.Follows.FirstOrDefaultAsync(f => f.UserId == userId && f.FollowingId == followingId);
                if (follow != null)
                {
                    _context.Follows.Remove(follow);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Follow relationship not found.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here
                throw new Exception("An error occurred while removing the following relationship.", ex);
            }
        }
    }
}
