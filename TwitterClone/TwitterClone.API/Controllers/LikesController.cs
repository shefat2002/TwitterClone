using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwitterClone.Domain.Entities;

namespace TwitterClone.API.Controllers;

// api/likes
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class LikesController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public LikesController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // GET: api/likes
    [HttpGet]
    public IActionResult GetLikes()
    {
        var likes = new List<Like>
        {
            new(Guid.NewGuid(), Guid.NewGuid()),
            new(Guid.NewGuid(), Guid.NewGuid()),
            new(Guid.NewGuid(), Guid.NewGuid())
        };
        return Ok(likes);
    }

    // POST: api/likes
    [HttpPost]
    public IActionResult LikeTweet()
    {
        return Ok(new
        {
            LikeId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            TweetId = Guid.NewGuid()
        });
    }

    // GET: api/likes/{id}
    [HttpGet("{id}")]
    public IActionResult GetLikeById([FromRoute] Guid id)
    {
        return Ok(new
        {
            LikeId = id,
            UserId = Guid.NewGuid(),
            TweetId = Guid.NewGuid()
        });
    }

    // DELETE: api/likes/{id}
    [HttpDelete("{id}")]
    public IActionResult UnlikeTweet([FromRoute] Guid id)
    {
        return Ok(new
        {
            LikeId = id,
            Message = "Like removed successfully."
        });
    }
}
