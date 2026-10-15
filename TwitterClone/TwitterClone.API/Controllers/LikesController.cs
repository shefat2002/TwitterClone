using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwitterClone.Application.Dtos.Like;
using TwitterClone.Application.Interfaces;
using TwitterClone.Domain.Entities;

namespace TwitterClone.API.Controllers;

// api/likes
[Route("api/[controller]")]
[ApiController]
// [Authorize]
public class LikesController : ControllerBase
{
    private readonly ILikeService _likeService;

    public LikesController(ILikeService likeService)
    {
        _likeService = likeService;
    }


    // GET: api/likes
    [HttpGet]
    [ProducesResponseType(typeof(List<LikeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetLikes([FromRoute] Guid id)
    {
        var likes = _likeService.GetLikeById(id);
        if (likes == null)
        {
            return NotFound();
        }
        return Ok(likes);
    }
    
    // GET: api/likes/{id}
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(LikeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetLikeById([FromRoute] Guid id)
    {
        var like = _likeService.GetLikeById(id);
        if (like == null)
        {
            return NotFound();
        }
        return Ok(like);
    }

    // POST: api/likes
    [HttpPost]
    [ProducesResponseType(typeof(LikeDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CreateLike([FromBody] CreateLikeDto createLikeDto)
    {
        var createdLike = _likeService.CreateLike(createLikeDto);
        if (createdLike == null)
        {
            return BadRequest("Failed to create like.");
        }

        return CreatedAtAction(nameof(GetLikeById), new { id = createdLike.Id }, createdLike);
    }


    // DELETE: api/likes/{id}
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UnlikeTweet([FromRoute] Guid id)
    {
        var isDeleted = _likeService.DeleteLike(id);
        if (!isDeleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}
