using TwitterClone.Application.Dtos.Follow;
using TwitterClone.Application.Dtos.User;

namespace TwitterClone.Application.Interfaces;

public interface IFollowService
{
    FollowDto? CreateFollow(CreateFollowDto createFollowDto);
    FollowDto? GetFollowById(Guid id);
    List<FollowDto> GetFollows(Guid? followerId, Guid? followingId);
    List<UserDto> GetFollowers(Guid userId);
    List<UserDto> GetFollowing(Guid userId);
    bool DeleteFollow(Guid id);
}