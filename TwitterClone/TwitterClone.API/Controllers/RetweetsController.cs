using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwitterClone.Domain.Entities;

namespace TwitterClone.API.Controllers;

// api/retweets
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RetweetsController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public RetweetsController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // GET: api/retweets
    [HttpGet]
    public IActionResult GetRetweets()
    {
        var retweets = new List<Retweet>
        {
            new() { UserId = Guid.NewGuid(), TweetId = Guid.NewGuid(), Comment = "Great tweet!" },
            new() { UserId = Guid.NewGuid(), TweetId = Guid.NewGuid(), Comment = null },
            new() { UserId = Guid.NewGuid(), TweetId = Guid.NewGuid(), Comment = "So true." }
        };
        return Ok(retweets);
    }

    // POST: api/retweets
    [HttpPost]
    public IActionResult CreateRetweet()
    {
        return Ok(new
        {
            RetweetId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            TweetId = Guid.NewGuid(),
            Comment = "This is a retweet."
        });
    }

    // GET: api/retweets/{id}
    [HttpGet("{id}")]
    public IActionResult GetRetweetById([FromRoute] Guid id)
    {
        return Ok(new
        {
            RetweetId = id,
            UserId = Guid.NewGuid(),
            TweetId = Guid.NewGuid(),
            Comment = "This is a single retweet."
        });
    }

    // DELETE: api/retweets/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteRetweet([FromRoute] Guid id)
    {
        return Ok(new
        {
            RetweetId = id,
            Message = "Retweet deleted successfully."
        });
    }
}
