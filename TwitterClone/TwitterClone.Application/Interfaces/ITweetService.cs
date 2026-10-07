using TwitterClone.Application.Dtos.Tweet;
using TwitterClone.Domain.Entities;

namespace TwitterClone.Application.Interfaces;

public interface ITweetService
{
    TweetDto CreateTweetDto(CreateTweetDto createTweetDto);
    TweetDto GetTweetById(Guid id);
    List<TweetDto> GetTweets();
    TweetDto UpdateTweet(Guid id, UpdateTweetDto updateTweetDto);
    bool DeleteTweet(Guid id);
    
}