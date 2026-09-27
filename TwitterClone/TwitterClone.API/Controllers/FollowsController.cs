using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwitterClone.Domain.Entities;

namespace TwitterClone.API.Controllers;

// api/follows
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class FollowsController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public FollowsController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // GET: api/follows
    [HttpGet]
    public IActionResult GetFollows()
    {
        var follows = new List<Follow>
        {
            new() { FollowerId = Guid.NewGuid(), FollowingId = Guid.NewGuid() },
            new() { FollowerId = Guid.NewGuid(), FollowingId = Guid.NewGuid() },
            new() { FollowerId = Guid.NewGuid(), FollowingId = Guid.NewGuid() }
        };
        return Ok(follows);
    }

    // POST: api/follows
    [HttpPost]
    public IActionResult FollowUser()
    {
        return Ok(new
        {
            FollowId = Guid.NewGuid(),
            FollowerId = Guid.NewGuid(),
            FollowingId = Guid.NewGuid()
        });
    }

    // GET: api/follows/{id}
    [HttpGet("{id}")]
    public IActionResult GetFollowById([FromRoute] Guid id)
    {
        return Ok(new
        {
            FollowId = id,
            FollowerId = Guid.NewGuid(),
            FollowingId = Guid.NewGuid()
        });
    }

    // DELETE: api/follows/{id}
    [HttpDelete("{id}")]
    public IActionResult UnfollowUser([FromRoute] Guid id)
    {
        return Ok(new
        {
            FollowId = id,
            Message = "Unfollowed successfully."
        });
    }
}
