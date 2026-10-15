using TwitterClone.Application.Dtos.Like;
using TwitterClone.Application.Interfaces;
using TwitterClone.Domain.Entities;

namespace TwitterClone.Application.Services;

public class LikeService : ILikeService
{
    private readonly ILikeRepository _likeRepository;
    private readonly IUserRepository _userRepository;
    private readonly ITweetRepository _tweetRepository;

    public LikeService(ILikeRepository likeRepository, IUserRepository userRepository, ITweetRepository tweetRepository)
    {
        _likeRepository = likeRepository;
        _userRepository = userRepository;
        _tweetRepository = tweetRepository;
    }

    public LikeDto? CreateLike(CreateLikeDto createLikeDto)
    {
        var user  = _userRepository.GetUserById(createLikeDto.UserId);
        var tweet = _tweetRepository.GetTweetById(createLikeDto.TweetId);
        if(user == null || tweet == null || !tweet.CanBeLiked())
        {
            return null;
        }
        var existingLike = _likeRepository.GetLikeByUserAndTweet(user.Id, tweet.Id);
        if (existingLike != null)
        {
            return new LikeDto
            {
                Id = existingLike.Id,
                UserId = existingLike.UserId,
                TweetId = existingLike.TweetId
            };
        }
        var createdLike = _likeRepository.AddLike(new Like
        {
            UserId = user.Id,
            TweetId = tweet.Id
        });
        return new LikeDto
        {
            Id = createdLike.Id,
            UserId = createdLike.UserId,
            TweetId = createdLike.TweetId
        };
            
    }

    public LikeDto? GetLikeById(Guid id)
    {
        var  like = _likeRepository.GetLikeById(id);
        return like == null ? null : new LikeDto
        {
            Id = like.Id,
            UserId = like.UserId,
            TweetId = like.TweetId
        };
    }

    public List<LikeDto> GetLikes(Guid? userId, Guid? tweetId)
    {
        return _likeRepository.GetLikes(userId, tweetId)
            .OrderByDescending(l => l.CreatedAt)
            .Select(like => new LikeDto
            {
                Id = like.Id,
                UserId = like.UserId,
                TweetId = like.TweetId
            }).ToList();
    }

    public bool DeleteLike(Guid id)
    {
        var like = _likeRepository.GetLikeById(id);
        if (like == null)
        {
            return false;
        }
        return _likeRepository.DeleteLike(like);
    }
}