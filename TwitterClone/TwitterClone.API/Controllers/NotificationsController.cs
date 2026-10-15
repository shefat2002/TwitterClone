using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwitterClone.Application.Dtos.NotificationDto;
using TwitterClone.Application.Interfaces;
using TwitterClone.Domain.Entities;

namespace TwitterClone.API.Controllers;

// api/notifications
[Route("api/[controller]")]
[ApiController]
// [Authorize]
public class NotificationsController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public NotificationsController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }


    // GET: api/notifications
    [HttpGet]
    [ProducesResponseType(typeof(List<NotificationDto>), StatusCodes.Status200OK)]
    public IActionResult GetNotifications([FromQuery] Guid? userId, [FromQuery] string? type, [FromQuery] bool? isRead)
    {
        return Ok(_notificationService.GetNotifications(userId, type, isRead));
    }

    // GET: api/notifications/{id}
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(NotificationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetNotificationById([FromRoute] Guid id)
    {
        var notification = _notificationService.GetNotificationById(id);
        if (notification == null)
        {
            return NotFound();
        }
        return Ok(notification);
    }
    
    // POST: api/notifications
    [HttpPost]
    [ProducesResponseType(typeof(NotificationDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CreateNotification([FromBody] CreateNotificationDto createNotificationDto)
    {
        var createdNotification = _notificationService.CreateNotification(createNotificationDto);
        if (createdNotification == null)
        {
            return BadRequest("Failed to create notification.");
        }
        return CreatedAtAction(nameof(GetNotificationById), new { id = createdNotification.Id }, createdNotification);
    }

    //PATCH: api/notifications/{id}/read
    [HttpPatch("{id}/read")]
    [ProducesResponseType(typeof(NotificationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult MarkNotificationAsRead([FromRoute] Guid id)
    {
        var updatedNotification = _notificationService.MarkNotificationAsRead(id);
        if (updatedNotification == null)
        {
            return NotFound();
        }
        return Ok(updatedNotification);
    }
    
    //PATCH: api/notifications/{id}/read-all
    [HttpPatch("{id}/read-all")]
    [ProducesResponseType(typeof(List<NotificationDto>), StatusCodes.Status200OK)]
    public IActionResult MarkAllNotificationsAsRead([FromRoute] Guid id)
    {
        var updatedCount = _notificationService.MarkAllNotificationsAsRead(id);
        return Ok(updatedCount);
    }

    // DELETE: api/notifications/{id}
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteNotification([FromRoute] Guid id)
    {
        var isDeleted = _notificationService.DeleteNotification(id);
        if (!isDeleted){
            return NotFound();
        }

        return NoContent();
    }
}
