using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwitterClone.Application.Dtos.Message;
using TwitterClone.Application.Interfaces;
using TwitterClone.Domain.Entities;

namespace TwitterClone.API.Controllers;

// api/messages
[Route("api/[controller]")]
[ApiController]
// [Authorize]
public class MessagesController : ControllerBase
{
    private readonly IMessageService _messageService;

    public MessagesController(IMessageService messageService)
    {
        _messageService = messageService;
    }


    // GET: api/messages
    [HttpGet]
    [ProducesResponseType(typeof(List<MessageDto>), StatusCodes.Status200OK)]
    public IActionResult GetMessages([FromQuery] Guid? senderId, [FromQuery] Guid? receiverId)
    {
        return Ok(_messageService.GetMessages(senderId, receiverId));
    }
    
    // GET: api/messages/{id}
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(MessageDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetMessageById([FromRoute] Guid id)
    {
        var message = _messageService.GetMessageById(id);
        if (message == null)
        {
            return NotFound();
        }
        return Ok(message);
    }
    
    // GET: api/messages/conversation/
    [HttpGet("conversation")]
    [ProducesResponseType(typeof(List<MessageDto>), StatusCodes.Status200OK)]
    public IActionResult GetConversation([FromQuery] Guid userId, [FromQuery] Guid otherUserId)
    {
        return Ok(_messageService.GetConversation(userId, otherUserId));
    }

    // POST: api/messages
    [HttpPost]
    [ProducesResponseType(typeof(MessageDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult SendMessage([FromBody] CreateMessageDto createMessageDto)
    {
        var sendMessage = _messageService.SendMessage(createMessageDto);
        if (sendMessage == null)
        {
            return BadRequest("Failed to send message.");
        } 
        return CreatedAtAction(nameof(GetMessageById), new { id = sendMessage.Id }, sendMessage);
        
    }


    // PUT: api/messages/{id}
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(MessageDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult MarkMessageAsRead([FromRoute] Guid id, [FromBody] UpdateMessageDto updateMessageDto)
    {
        var updatedMessage = _messageService.UpdateMessage(id, updateMessageDto);
        if (updatedMessage == null)
        {
            return NotFound();
        }
        return Ok(updatedMessage);
    }
    
    //PATCH: api/messages/{id}/read
    [HttpPatch("{id}/read")]
    [ProducesResponseType(typeof(MessageDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult MarkMessageAsRead([FromRoute] Guid id)
    {
        var updatedMessage = _messageService.MarkMessageAsRead(id);
        if (updatedMessage == null)
        {
            return NotFound();
        }
        return Ok(updatedMessage);
    }

    // DELETE: api/messages/{id}
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteMessage([FromRoute] Guid id)
    {
        var isDeleted = _messageService.DeleteMessage(id);
        if (!isDeleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}
