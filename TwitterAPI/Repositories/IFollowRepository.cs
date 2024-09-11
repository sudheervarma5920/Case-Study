using TwitterAPI.Entities;

namespace TwitterAPI.Repositories
{
    public interface IFollowRepository
    {
        Task AddFollowing(Follow follow);
        Task RemoveFollowing(string userId, string followingId);
        Task<IEnumerable<User>> GetFollowings(string userId); //Users followed by a specific user
        Task<IEnumerable<User>> GetFollowers(string followingId); //Users who follow a specific user
    }
}
