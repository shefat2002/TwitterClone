using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwitterClone.Domain.Entities;

namespace TwitterClone.API.Controllers;

// api/bookmarks
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BookmarksController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public BookmarksController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // GET: api/bookmarks
    [HttpGet]
    public IActionResult GetBookmarks()
    {
        var bookmarks = new List<Bookmark>
        {
            new(Guid.NewGuid(), Guid.NewGuid()),
            new(Guid.NewGuid(), Guid.NewGuid()),
            new(Guid.NewGuid(), Guid.NewGuid())
        };
        return Ok(bookmarks);
    }

    // POST: api/bookmarks
    [HttpPost]
    public IActionResult BookmarkTweet()
    {
        return Ok(new
        {
            BookmarkId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            TweetId = Guid.NewGuid()
        });
    }

    // GET: api/bookmarks/{id}
    [HttpGet("{id}")]
    public IActionResult GetBookmarkById([FromRoute] Guid id)
    {
        return Ok(new
        {
            BookmarkId = id,
            UserId = Guid.NewGuid(),
            TweetId = Guid.NewGuid()
        });
    }

    // DELETE: api/bookmarks/{id}
    [HttpDelete("{id}")]
    public IActionResult RemoveBookmark([FromRoute] Guid id)
    {
        return Ok(new
        {
            BookmarkId = id,
            Message = "Bookmark removed successfully."
        });
    }
}
