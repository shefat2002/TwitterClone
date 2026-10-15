using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwitterClone.Application.Dtos.Retweet;
using TwitterClone.Application.Interfaces;
using TwitterClone.Domain.Entities;

namespace TwitterClone.API.Controllers;

// api/retweets
[Route("api/[controller]")]
[ApiController]
// [Authorize]
public class RetweetsController : ControllerBase
{
    private readonly IRetweetService _retweetService;

    public RetweetsController(IRetweetService retweetService)
    {
        _retweetService = retweetService;
    }


    // GET: api/retweets
    [HttpGet]
    [ProducesResponseType(typeof(List<RetweetDto>), StatusCodes.Status200OK)]
    public IActionResult GetRetweets([FromQuery] Guid? userId, [FromQuery] Guid? tweetId)
    {
        return Ok(_retweetService.GetRetweets(userId, tweetId));
    }
    
    // GET: api/retweets/{id}
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(RetweetDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetRetweetById([FromRoute] Guid id)
    {
        var retweet = _retweetService.GetRetweetById(id);
        if (retweet == null)
        {
            return NotFound();
        }
        return Ok(retweet);
    }

    // POST: api/retweets
    [HttpPost]
    [ProducesResponseType(typeof(RetweetDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CreateRetweet([FromBody] CreateRetweetDto createRetweetDto)
    {
        var createdRetweet = _retweetService.CreateRetweet(createRetweetDto);
        if (createdRetweet == null)
        {
            return BadRequest("Failed to create retweet.");
        }
        return CreatedAtAction(nameof(GetRetweetById), new { id = createdRetweet.Id }, createdRetweet);
    }

    // PUT: api/retweets/{id}
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(RetweetDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetRetweetById([FromRoute] Guid id, [FromBody] UpdateRetweetDto updateRetweetDto)
    {
        var retweet = _retweetService.UpdateRetweet(id, updateRetweetDto);
        if (retweet == null)
        {
            return NotFound();
        }
        return Ok(retweet);
    }

    // DELETE: api/retweets/{id}
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteRetweet([FromRoute] Guid id)
    {
        var isDeleted = _retweetService.DeleteRetweet(id);
        if (!isDeleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}
