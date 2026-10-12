using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwitterClone.API.Data;
using TwitterClone.Application.Dtos.Tweet;
using TwitterClone.Application.Interfaces;
using TwitterClone.Domain.Entities;

namespace TwitterClone.API.Controllers;

// api/tweets
[Route("api/[controller]")]
[ApiController]
// [Authorize]
public class TweetsController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ITweetService _tweetService;

    public TweetsController(ITweetService tweetService, IConfiguration configuration)
    {
        _tweetService = tweetService;
        _configuration = configuration;
    }

    [HttpGet]
    public IActionResult GetTweets()
    {
        var appName = _configuration.GetValue<string>("TwitterSettings:AppName");
        var maxLength = _configuration.GetValue<int>("TwitterSettings:MaxTweetLength");
        var tweets = _tweetService.GetTweets();

        return Ok(tweets);
    }

    // POST: api/tweets
    [HttpPost]
    public IActionResult CreateTweet([FromBody] CreateTweetDto tweet)
    {
        var createdTweet = _tweetService.CreateTweetDto(tweet);
        if(createdTweet == null)
        {
            return BadRequest("Invalid tweet data.");
        }
        return Ok(createdTweet);
    }

    // GET: api/tweets/{id}
    [HttpGet("{id}")]
    public IActionResult GetTweetById([FromRoute] Guid id)
    {
        var tweet = _tweetService.GetTweetById(id);
        if (tweet == null)
        {
            return NotFound($"Tweet with ID {id} not found.");
        }
        return Ok(tweet);
    }

    // PUT: api/tweets/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateTweet([FromRoute] Guid id, [FromBody] UpdateTweetDto tweet)
    {
        var existingTweet = _tweetService.GetTweetById(id);
        if (existingTweet == null)
        {
            return NotFound($"Tweet with ID {id} not found.");
        }
        existingTweet.Content = tweet.Content;
        var updatedTweet = _tweetService.UpdateTweet(id, tweet);
        return Ok(updatedTweet);
    }

    // DELETE: api/tweets/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteTweet([FromRoute] Guid id)
    {
        var isDeleted = _tweetService.DeleteTweet(id);
        if (isDeleted == false)
        {
            return NotFound($"Tweet with ID {id} not found.");
        }
        return Ok(isDeleted);
    }
}