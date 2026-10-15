using TwitterClone.Application.Dtos.Retweet;
using TwitterClone.Application.Interfaces;
using TwitterClone.Domain.Entities;

namespace TwitterClone.Application.Services;

public class RetweetService : IRetweetService
{
    private readonly IRetweetRepository _retweetRepository;
    private readonly IUserRepository _userRepository;
    private readonly ITweetRepository _tweetRepository;

    public RetweetService(IRetweetRepository retweetRepository, IUserRepository userRepository, ITweetRepository tweetRepository)
    {
        _retweetRepository = retweetRepository;
        _userRepository = userRepository;
        _tweetRepository = tweetRepository;
    }

    public RetweetDto? CreateRetweet(CreateRetweetDto createRetweetDto)
    {
        if(!IsValidComment(createRetweetDto.Comment))
        {
            return null;
        }
        if(_userRepository.GetUserById(createRetweetDto.UserId) == null || _tweetRepository.GetTweetById(createRetweetDto.TweetId) == null)
        {
            return null;
        }
        var createdRetweet = _retweetRepository.AddRetweet(new Retweet
        {
            UserId = createRetweetDto.UserId,
            TweetId = createRetweetDto.TweetId,
            Comment = createRetweetDto.Comment?.Trim() ?? string.Empty,
        });
        return new RetweetDto
        {
            Id = createdRetweet.Id,
            UserId = createdRetweet.UserId,
            TweetId = createdRetweet.TweetId,
            Comment = createdRetweet.Comment
        };
    }

    public RetweetDto? GetRetweetById(Guid id)
    {
        var retweet = _retweetRepository.GetRetweetById(id);
        if (retweet == null)
        {
            return null;
        }

        return new RetweetDto
        {
            Id = retweet.Id,
            UserId = retweet.UserId,
            TweetId = retweet.TweetId,
            Comment = retweet.Comment
        };
    }

    public List<RetweetDto> GetRetweets(Guid? userId, Guid? tweetId)
    {
        return _retweetRepository.GetRetweets(userId, tweetId)
            .OrderByDescending(r => r.CreatedAt)
            .Select(retweet => new RetweetDto
            {
                Id = retweet.Id,
                UserId = retweet.UserId,
                TweetId = retweet.TweetId,
                Comment = retweet.Comment
            })
            .ToList();
    }

    public RetweetDto? UpdateRetweet(Guid id, UpdateRetweetDto updateRetweetDto)
    {
        if(!IsValidComment(updateRetweetDto.Comment))
        {
            return null;
        }
        var retweet = _retweetRepository.GetRetweetById(id);
        if (retweet == null)
        {
            return null;
        }
        retweet.Comment = updateRetweetDto.Comment?.Trim() ?? string.Empty;
        retweet.MarkAsModified();
        _retweetRepository.UpdateRetweet(retweet);
        return new RetweetDto
        {
            Id = retweet.Id,
            UserId = retweet.UserId,
            TweetId = retweet.TweetId,
            Comment = retweet.Comment
        };
    }

    public bool DeleteRetweet(Guid id)
    {
        var retweet = _retweetRepository.GetRetweetById(id);
        if(retweet == null)
        {
            return false;
        }
        return _retweetRepository.DeleteRetweet(retweet);
    }
    private static bool IsValidComment(string? comment)
    {
        return comment == null || comment.Length <= Tweet.MaxContentLength;
    }
}