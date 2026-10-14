using TwitterClone.Application.Dtos.Follow;
using TwitterClone.Application.Dtos.User;
using TwitterClone.Application.Interfaces;
using TwitterClone.Domain.Entities;

namespace TwitterClone.Application.Services;

public class FollowService : IFollowService
{
    private readonly IFollowRepository _followRepository;
    private readonly IUserRepository _userRepository;

    public FollowService(IFollowRepository followRepository, IUserRepository userRepository)
    {
        _followRepository = followRepository;
        _userRepository = userRepository;
    }

    public FollowDto? CreateFollow(CreateFollowDto createFollowDto)
    {
        if(createFollowDto.FollowerId == createFollowDto.FollowingId)
        {
            return null;
        }
        if (_userRepository.GetUserById(createFollowDto.FollowerId) == null ||
            _userRepository.GetUserById(createFollowDto.FollowingId) == null)
        {
            return null;
        }

        var existingFollow = _followRepository.GetFollowByUsers(createFollowDto.FollowerId, createFollowDto.FollowingId);
        if (existingFollow != null)
        {
            return new FollowDto
            {
                Id = existingFollow.Id,
                FollowerId = existingFollow.FollowerId,
                FollowingId = existingFollow.FollowingId
            };
        }

        var createdFollow = _followRepository.AddFollow(new Follow
        {
            FollowerId = createFollowDto.FollowerId,
            FollowingId = createFollowDto.FollowingId
        });

        return new FollowDto
        {
            Id = createdFollow.Id,
            FollowerId = createdFollow.FollowerId,
            FollowingId = createdFollow.FollowingId
        };
    }

    public FollowDto? GetFollowById(Guid id)
    {
        var follow = _followRepository.GetFollowById(id);
        if (follow == null)
        {
            return null;
        }
        return new FollowDto
        {
            Id = follow.Id,
            FollowerId = follow.FollowerId,
            FollowingId = follow.FollowingId
        };
    }

    public List<FollowDto> GetFollows(Guid? followerId, Guid? followingId)
    {
        var follows = _followRepository.GetFollows(followerId, followingId);
        return follows.Select(f => new FollowDto
        {
            Id = f.Id,
            FollowerId = f.FollowerId,
            FollowingId = f.FollowingId
        }).ToList();
    }

    public List<UserDto> GetFollowers(Guid userId)
    {
        
    }

    public List<UserDto> GetFollowing(Guid userId)
    {
        throw new NotImplementedException();
    }

    public bool DeleteFollow(Guid id)
    {
        throw new NotImplementedException();
    }
    
}