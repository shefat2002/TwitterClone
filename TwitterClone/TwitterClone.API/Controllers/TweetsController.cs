using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwitterClone.Domain.Entities;

namespace TwitterClone.API.Controllers;

// api/tweets
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TweetsController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public TweetsController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public IActionResult GetTweets()
    {
        var appName = _configuration.GetValue<string>("TwitterSettings:AppName");
        var maxLength = _configuration.GetValue<int>("TwitterSettings:MaxTweetLength");
        var tweets = new List<object>
        {
            new{userId = Guid.NewGuid(), content = "This is the first tweet."},
        };
        
        return Ok(new{
            appName,
            maxLength,
            tweets
        });
    }

    // POST: api/tweets
    [HttpPost]
    public IActionResult CreateTweet()
    {
        return Ok(new
        {
            TweetId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Content = "This is a new tweet."
        });
    }

    // GET: api/tweets/{id}
    [HttpGet("{id}")]
    public IActionResult GetTweetById([FromRoute] Guid id)
    {
        return Ok(new
        {
            TweetId = id,
            UserId = Guid.NewGuid(),
            Content = "This is a single tweet."
        });
    }

    // PUT: api/tweets/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateTweet([FromRoute] Guid id)
    {
        return Ok(new
        {
            TweetId = id,
            UserId = Guid.NewGuid(),
            Content = "This is an updated tweet."
        });
    }

    // DELETE: api/tweets/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteTweet([FromRoute] Guid id)
    {
        return Ok(new
        {
            TweetId = id,
            Message = "Tweet deleted successfully."
        });
    }
}