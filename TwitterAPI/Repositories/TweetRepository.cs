using Microsoft.EntityFrameworkCore;
using TwitterAPI.Entities;

namespace TwitterAPI.Repositories
{
    public class TweetRepository : ITweetRepository
    {
        private readonly TwitterContext _context;
        private readonly IConfiguration _configuration;
        public TweetRepository(TwitterContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task Add(Tweet tweet)
        {
            try
            {
                await _context.Tweets.AddAsync(tweet);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new Exception("An error occurred while adding the tweet.", ex);
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                var tweet = await _context.Tweets.FindAsync(id);
                if (tweet == null)
                    throw new Exception("Tweet not found.");

                _context.Tweets.Remove(tweet);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new Exception("An error occurred while deleting the tweet.", ex);
            }
        }

        public async Task<List<Tweet>> GetByUserIdAsync(string userId)
        {
            try
            {
                var tweets = await _context.Tweets
                                           .Where(t => t.UserId == userId)
                                           .ToListAsync();
                return tweets;
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new Exception("An error occurred while retrieving the tweets by UserId.", ex);
            }
        }
        public async Task<List<Tweet>> GetByTweetId(int tweetId)
        {
            try
            {
                var tweets = await _context.Tweets
                                           .Where(t => t.TweetId == tweetId)
                                           .ToListAsync();
                return tweets;
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new Exception("An error occurred while retrieving the tweets by UserId.", ex);
            }
        }

        public async Task<List<Tweet>> GetAll()
        {
            try
            {
                return await _context.Tweets.ToListAsync();
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new Exception("An error occurred while retrieving the tweets.", ex);
            }
        }

        public async Task Update(Tweet tweet)
        {
            try
            {
                _context.Tweets.Update(tweet);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new Exception("An error occurred while updating the tweet.", ex);
            }
        }
    }
}
