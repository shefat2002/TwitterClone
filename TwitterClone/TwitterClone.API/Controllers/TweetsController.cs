using Microsoft.AspNetCore.Mvc;

namespace TwitterClone.API.Controllers;

[Route("api/[controller]")]
[ApiController]
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
        var maxLength = _configuration.GetValue<int>("TwitterSettings:MaxTweetLength");
        var tweets = new List<object>
        {
            new{userId = Guid.NewGuid(), content = "This is the first tweet."},
        };
        
        return Ok(new{
            maxLength,
            tweets
        });
    }
}