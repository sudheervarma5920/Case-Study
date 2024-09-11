using TwitterAPI.Entities;

namespace TwitterAPI.Repositories
{
    public interface ILikesRepository
    {
        public Task AddLike(Likes likes);
        public Task<List<Likes>> GetLikes(int TweetId);
        public Task DeleteLike(string userId, int tweetId);
    }
}
