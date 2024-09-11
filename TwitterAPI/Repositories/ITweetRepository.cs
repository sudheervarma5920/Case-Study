using TwitterAPI.Entities;

namespace TwitterAPI.Repositories
{
    public interface ITweetRepository
    {
        Task Add(Tweet tweet);
        Task<List<Tweet>> GetAll();

        Task<List<Tweet>> GetByUserIdAsync(string userId);
        Task<List<Tweet>> GetByTweetId(int tweetId);
        Task Update(Tweet tweet);
        Task Delete(int id);
    }
}
