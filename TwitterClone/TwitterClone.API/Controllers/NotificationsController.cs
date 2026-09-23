using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TwitterClone.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class NotificationsController : ControllerBase
{

    // api/notifications
    [HttpGet]
    public IActionResult GetNotifications()
    {
        var notifications = new List<object>
        {
            new { notificationId = Guid.NewGuid(), message = "You have a new follower!" },
            new { notificationId = Guid.NewGuid(), message = "Your tweet has been liked!" },
            new { notificationId = Guid.NewGuid(), message = "You have a new mention!" }
        };

        return Ok(notifications);
    }
    
}