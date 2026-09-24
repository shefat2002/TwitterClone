using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwitterClone.Domain.Entities;

namespace TwitterClone.API.Controllers;

// api/notifications
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class NotificationsController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public NotificationsController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // GET: api/notifications
    [HttpGet]
    public IActionResult GetNotifications()
    {
        var notifications = new List<object>
        {
            new { NotificationId = Guid.NewGuid(), UserId = Guid.NewGuid(), Type = "Like", Message = "User liked your tweet", IsRead = false },
            new { NotificationId = Guid.NewGuid(), UserId = Guid.NewGuid(), Type = "Comment", Message = "User commented on your tweet", IsRead = false },
            new { NotificationId = Guid.NewGuid(), UserId = Guid.NewGuid(), Type = "Mention", Message = "User mentioned you in a tweet", IsRead = true },
            new { NotificationId = Guid.NewGuid(), UserId = Guid.NewGuid(), Type = "FriendRequest", Message = "User sent you a friend request", IsRead = false },
            new { NotificationId = Guid.NewGuid(), UserId = Guid.NewGuid(), Type = "System", Message = "Welcome to TwitterClone!", IsRead = true }
        };
        return Ok(notifications);
    }

    // GET: api/notifications/likes
    [HttpGet("likes")]
    public IActionResult GetLikeNotifications()
    {
        var notifications = new List<object>
        {
            new { NotificationId = Guid.NewGuid(), UserId = Guid.NewGuid(), Type = "Like", LikebyUserId = Guid.NewGuid(), Message = "User liked your tweet", IsRead = false },
            new { NotificationId = Guid.NewGuid(), UserId = Guid.NewGuid(), Type = "Like", LikebyUserId = Guid.NewGuid(), Message = "User liked your tweet", IsRead = true }
        };
        return Ok(notifications);
    }

    // GET: api/notifications/comments
    [HttpGet("comments")]
    public IActionResult GetCommentNotifications()
    {
        var notifications = new List<object>
        {
            new { NotificationId = Guid.NewGuid(), UserId = Guid.NewGuid(), Type = "Comment", CommentByUserId = Guid.NewGuid(), Message = "User commented on your tweet", IsRead = false },
            new { NotificationId = Guid.NewGuid(), UserId = Guid.NewGuid(), Type = "Comment", CommentByUserId = Guid.NewGuid(), Message = "User commented on your tweet", IsRead = false }
        };
        return Ok(notifications);
    }

    // GET: api/notifications/mentions
    [HttpGet("mentions")]
    public IActionResult GetMentionNotifications()
    {
        var notifications = new List<object>
        {
            new { NotificationId = Guid.NewGuid(), UserId = Guid.NewGuid(), Type = "Mention", MentionedByUserId = Guid.NewGuid(), Message = "User mentioned you in a tweet", IsRead = false },
            new { NotificationId = Guid.NewGuid(), UserId = Guid.NewGuid(), Type = "Mention", MentionedByUserId = Guid.NewGuid(), Message = "User mentioned you in a tweet", IsRead = true }
        };
        return Ok(notifications);
    }

    // GET: api/notifications/friend-requests
    [HttpGet("friend-requests")]
    public IActionResult GetFriendRequestNotifications()
    {
        var notifications = new List<object>
        {
            new { NotificationId = Guid.NewGuid(), UserId = Guid.NewGuid(), Type = "FriendRequest", RequestedByUserId = Guid.NewGuid(), Message = "User sent you a friend request", IsRead = false },
            new { NotificationId = Guid.NewGuid(), UserId = Guid.NewGuid(), Type = "FriendRequest", RequestedByUserId = Guid.NewGuid(), Message = "User sent you a friend request", IsRead = false }
        };
        return Ok(notifications);
    }

    // GET: api/notifications/system
    [HttpGet("system")]
    public IActionResult GetSystemNotifications()
    {
        var notifications = new List<object>
        {
            new { NotificationId = Guid.NewGuid(), UserId = Guid.NewGuid(), Type = "System", Message = "Welcome to TwitterClone!", IsRead = true },
            new { NotificationId = Guid.NewGuid(), UserId = Guid.NewGuid(), Type = "System", Message = "Your account was created successfully.", IsRead = true }
        };
        return Ok(notifications);
    }

    // GET: api/notifications/{id}
    [HttpGet("{id}")]
    public IActionResult GetNotificationById([FromRoute] Guid id)
    {
        return Ok(new
        {
            NotificationId = id,
            UserId = Guid.NewGuid(),
            Type = "Like",
            Message = "User liked your tweet",
            IsRead = false
        });
    }

    // PUT: api/notifications/{id}
    [HttpPut("{id}")]
    public IActionResult MarkNotificationAsRead([FromRoute] Guid id)
    {
        return Ok(new
        {
            NotificationId = id,
            UserId = Guid.NewGuid(),
            Type = "Like",
            Message = "User liked your tweet",
            IsRead = true
        });
    }

    // DELETE: api/notifications/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteNotification([FromRoute] Guid id)
    {
        return Ok(new
        {
            NotificationId = id,
            Message = "Notification deleted successfully."
        });
    }
}
