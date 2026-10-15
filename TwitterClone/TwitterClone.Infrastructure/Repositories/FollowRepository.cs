using TwitterClone.Application.Interfaces;
using TwitterClone.Domain.Entities;

namespace TwitterClone.Infrastructure.Repositories;

public class FollowRepository : IFollowRepository
{
    private readonly List<Follow> _follows = new List<Follow>();
    public Follow AddFollow(Follow follow)
    {
        _follows.Add(follow);
        return follow;
    }

    public bool DeleteFollow(Follow follow)
    {
        return _follows.Remove(follow);
    }

    public int DeleteFollowByUserId(Guid userId)
    {
        return _follows.RemoveAll(f => f.FollowerId == userId || f.FollowingId == userId);
    }

    public Follow? GetFollowById(Guid id)
    {
        return _follows.SingleOrDefault(f => f.Id == id);
    }

    public Follow? GetFollowByUsers(Guid followerId, Guid followingId)
    {
        return _follows.FirstOrDefault(f => f.FollowerId == followerId && f.FollowingId == followingId);
    }

    public List<Follow> GetFollows(Guid? followerId, Guid? followingId)
    {
        return _follows.Where(f => (followerId == null || f.FollowerId == followerId) && (followingId == null || f.FollowingId == followingId)).ToList();
    }
}