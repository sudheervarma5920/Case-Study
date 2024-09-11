using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterAPI.Entities;

namespace TwitterAPI.Repositories
{
    public class LikesRepository : ILikesRepository
    {
        private readonly TwitterContext _twitterContext;

        public LikesRepository(TwitterContext twitterContext)
        {
            _twitterContext = twitterContext;
        }

        public async Task AddLike(Likes likes)
        {
            _twitterContext.Likes.Add(likes);
            await _twitterContext.SaveChangesAsync();
        }

        public async Task DeleteLike(string userId, int tweetId)
        {
            var like = await _twitterContext.Likes
                .FirstOrDefaultAsync(x => x.UserId == userId && x.TweetId == tweetId);
            if (like != null)
            {
                _twitterContext.Likes.Remove(like);
                await _twitterContext.SaveChangesAsync();
            }
        }

        public async Task<List<Likes>> GetLikes(int tweetId)
        {
            return await _twitterContext.Likes
                .Where(x => x.TweetId == tweetId)
                .ToListAsync();
        }
    }
}
