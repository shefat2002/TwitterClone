using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwitterClone.Domain.Entities;

namespace TwitterClone.API.Controllers;

// api/messages
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MessagesController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public MessagesController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // GET: api/messages
    [HttpGet]
    public IActionResult GetMessages()
    {
        var messages = new List<object>
        {
            new { MessageId = Guid.NewGuid(), SenderId = Guid.NewGuid(), ReceiverId = Guid.NewGuid(), Content = "Hello!", IsRead = false },
            new { MessageId = Guid.NewGuid(), SenderId = Guid.NewGuid(), ReceiverId = Guid.NewGuid(), Content = "How are you?", IsRead = false },
            new { MessageId = Guid.NewGuid(), SenderId = Guid.NewGuid(), ReceiverId = Guid.NewGuid(), Content = "See you later.", IsRead = true }
        };
        return Ok(messages);
    }

    // POST: api/messages
    [HttpPost]
    public IActionResult SendMessage()
    {
        return Ok(new
        {
            MessageId = Guid.NewGuid(),
            SenderId = Guid.NewGuid(),
            ReceiverId = Guid.NewGuid(),
            Content = "This is a new message.",
            IsRead = false
        });
    }

    // GET: api/messages/{id}
    [HttpGet("{id}")]
    public IActionResult GetMessageById([FromRoute] Guid id)
    {
        return Ok(new
        {
            MessageId = id,
            SenderId = Guid.NewGuid(),
            ReceiverId = Guid.NewGuid(),
            Content = "This is a single message.",
            IsRead = false
        });
    }

    // PUT: api/messages/{id}
    [HttpPut("{id}")]
    public IActionResult MarkMessageAsRead([FromRoute] Guid id)
    {
        return Ok(new
        {
            MessageId = id,
            SenderId = Guid.NewGuid(),
            ReceiverId = Guid.NewGuid(),
            Content = "This message is read.",
            IsRead = true
        });
    }

    // DELETE: api/messages/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteMessage([FromRoute] Guid id)
    {
        return Ok(new
        {
            MessageId = id,
            Message = "Message deleted successfully."
        });
    }
}
