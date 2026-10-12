using TwitterClone.Application.Dtos.Tweet;
using TwitterClone.Application.Interfaces;
using TwitterClone.Domain.Entities;

namespace TwitterClone.Application.Services;

public class TweetService : ITweetService
{
    private readonly ITweetRepository _tweetRepository;

    public TweetService(ITweetRepository tweetRepository)
    {
        _tweetRepository = tweetRepository;
    }

    public TweetDto CreateTweetDto(CreateTweetDto createTweetDto)
    {
        if(string.IsNullOrWhiteSpace(createTweetDto.Content))
        {
            return null;
        }
        var createdTweet = _tweetRepository.AddTweet(new Tweet
        {
            UserId = createTweetDto.UserId,
            Content = createTweetDto.Content
        });
        return (new TweetDto
        {
            Id = createdTweet.Id,
            UserId = createdTweet.UserId,
            Content = createdTweet.Content
        });
    }

    public TweetDto GetTweetById(Guid id)
    {
        var tweet = _tweetRepository.GetTweetById(id);
        if (tweet == null)
        {
            return null;
        }
        return new TweetDto
        {
            Id = tweet.Id,
            UserId = tweet.UserId,
            Content = tweet.Content
        };
    }

    public List<TweetDto> GetTweets()
    {
        var tweets = _tweetRepository.GetTweets();

        return new List<TweetDto>(tweets.Select(t => new TweetDto
        {
            Id = t.Id,
            UserId = t.UserId,
            Content = t.Content
        }));
    }

    public TweetDto UpdateTweet(Guid id, UpdateTweetDto updateTweetDto)
    {
        var existingTweet = _tweetRepository.GetTweetById(id);
        if (existingTweet == null)
        {
            return null;
        }
        existingTweet.Content = updateTweetDto.Content;
        var updatedTweet = _tweetRepository.UpdateTweet(existingTweet);
        return new TweetDto
        {
            Id = id,
            UserId =  updatedTweet.UserId,
            Content =  updatedTweet.Content
        };
    }

    public bool DeleteTweet(Guid id)
    {
        var  tweet = _tweetRepository.GetTweetById(id);
        if (tweet == null)
        {
            return false;
        }
        var isDeleted = _tweetRepository.DeleteTweet(tweet);
        return true;
    }
}