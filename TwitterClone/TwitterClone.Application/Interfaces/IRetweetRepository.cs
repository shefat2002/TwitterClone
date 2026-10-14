using TwitterClone.Domain.Entities;

namespace TwitterClone.Application.Interfaces;

public interface IRetweetRepository
{
    Retweet AddRetweet(Retweet retweet);
    Retweet UpdateRetweet(Retweet retweet);
    bool DeleteRetweet(Retweet retweet);
    int DeleteRetweetByUserId(Guid userId);
    int DeleteRetweetByTweetId(Guid tweetId);
    Retweet? GetRetweetById(Guid id);
    List<Retweet> GetRetweets(Guid? userId, Guid? tweetId);
}