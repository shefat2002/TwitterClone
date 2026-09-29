using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwitterClone.API.Data;
using TwitterClone.API.Dtos.Tweet;
using TwitterClone.Domain.Entities;

namespace TwitterClone.API.Controllers;

// api/tweets
[Route("api/[controller]")]
[ApiController]
// [Authorize]
public class TweetsController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly TweetRepository _tweetRepository;

    public TweetsController(IConfiguration configuration, TweetRepository tweetRepository)
    {
        _configuration = configuration;
        _tweetRepository = tweetRepository;
    }

    [HttpGet]
    public IActionResult GetTweets()
    {
        var appName = _configuration.GetValue<string>("TwitterSettings:AppName");
        var maxLength = _configuration.GetValue<int>("TwitterSettings:MaxTweetLength");
        var tweets = _tweetRepository.GetTweets();

        return Ok(tweets.Select(t => new TweetDto
        {
            Id = t.Id,
            UserId = t.UserId,
            Content = t.Content
        }));
    }

    // POST: api/tweets
    [HttpPost]
    public IActionResult CreateTweet([FromBody] CreateTweetDto tweet)
    {
        if(string.IsNullOrWhiteSpace(tweet.Content) || tweet.Content.Length > _configuration.GetValue<int>("TwitterSettings:MaxTweetLength"))
        {
            return BadRequest($"Tweet content must be between 1 and {_configuration.GetValue<int>("TwitterSettings:MaxTweetLength")} characters.");
        }
        var createdTweet = _tweetRepository.AddTweet(new Tweet
        {
            UserId = tweet.UserId,
            Content = tweet.Content
        });
        return Ok(new TweetDto()
        {
            Id = createdTweet.Id,
            UserId = createdTweet.UserId,
            Content = createdTweet.Content
        });
    }

    // GET: api/tweets/{id}
    [HttpGet("{id}")]
    public IActionResult GetTweetById([FromRoute] Guid id)
    {
        var tweet = _tweetRepository.GetTweetById(id);
        if (tweet == null)
        {
            return NotFound($"Tweet with ID {id} not found.");
        }
        return Ok(new TweetDto{
            Id = tweet.Id,
            UserId = tweet.UserId,
            Content = tweet.Content
        });
    }

    // PUT: api/tweets/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateTweet([FromRoute] Guid id, [FromBody] UpdateTweetDto tweet)
    {
        var existingTweet = _tweetRepository.GetTweetById(id);
        if (existingTweet == null)
        {
            return NotFound($"Tweet with ID {id} not found.");
        }
        existingTweet.Content = tweet.Content;
        var updatedTweet = _tweetRepository.UpdateTweet(existingTweet);
        return Ok(new TweetDto
        {
            Id = id,
            UserId =  updatedTweet.UserId,
            Content =  updatedTweet.Content
        });
    }

    // DELETE: api/tweets/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteTweet([FromRoute] Guid id)
    {
        var  tweet = _tweetRepository.GetTweetById(id);
        if (tweet == null)
        {
            return NotFound($"Tweet with ID {id} not found.");
        }
        var isDeleted = _tweetRepository.DeleteTweet(tweet);
        return Ok(isDeleted);
    }
}