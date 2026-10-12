using TwitterClone.Domain.Entities;

namespace TwitterClone.Application.Interfaces;

public interface ITweetRepository
{
    Tweet AddTweet(Tweet tweet);
    Tweet GetTweetById(Guid id);
    IEnumerable<Tweet> GetTweetsByUserId(Guid userId);
    IEnumerable<Tweet> GetTweets();
    Tweet UpdateTweet(Tweet tweet);
    bool DeleteTweet(Tweet id);
    
}