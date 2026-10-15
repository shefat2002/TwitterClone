using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwitterClone.Application.Dtos.Follow;
using TwitterClone.Application.Interfaces;

namespace TwitterClone.API.Controllers;

// api/follows
[Route("api/[controller]")]
[ApiController]
// [Authorize]
public class FollowsController : ControllerBase
{
    private readonly IFollowService _followService;

    public FollowsController(IFollowService followService)
    {
        _followService = followService;
    }

    // GET: api/follows
    [HttpGet]
    [ProducesResponseType(typeof(List<FollowDto>), StatusCodes.Status200OK)]
    public IActionResult GetFollows([FromQuery] Guid? followerId, [FromQuery] Guid? followingId)
    {
        return Ok(_followService.GetFollows(followerId, followingId));
    }
    
    //GET: api/follows/{id}
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(FollowDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetFollowById([FromRoute] Guid id)
    {
        var follow = _followService.GetFollowById(id);
        if (follow == null)
        {
            return NotFound();
        }
        return Ok(follow);
    }
    
    //GET: api/follows/followers/{userId}
    [HttpGet("followers/{userId}")]
    [ProducesResponseType(typeof(List<FollowDto>), StatusCodes.Status200OK)]
    public IActionResult GetFollowers([FromRoute] Guid userId)
    {
        return Ok(_followService.GetFollowers(userId));
    }
    
    //GET: api/follows/following/{userId}
    [HttpGet("following/{userId}")]
    [ProducesResponseType(typeof(List<FollowDto>), StatusCodes.Status200OK)]
    public IActionResult GetFollowing([FromRoute] Guid userId)
    {
        return Ok(_followService.GetFollowing(userId));
    }

    // POST: api/follows
    [HttpPost]
    [ProducesResponseType(typeof(FollowDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult FollowUser([FromBody] CreateFollowDto createFollowDto)
    {
        var createdFollow = _followService.CreateFollow(createFollowDto);
        if (createdFollow == null)
        {
            return BadRequest("Failed to follow user.");
        }
        return CreatedAtAction(nameof(GetFollowById), new { id = createdFollow.Id }, createdFollow);
    }
    

    // DELETE: api/follows/{id}
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UnfollowUser([FromRoute] Guid id)
    {
        var isDeleted = _followService.DeleteFollow(id);
        if (!isDeleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}
