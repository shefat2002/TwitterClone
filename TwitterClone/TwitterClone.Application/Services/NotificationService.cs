using TwitterClone.Application.Dtos.NotificationDto;
using TwitterClone.Application.Interfaces;
using TwitterClone.Domain.Entities;

namespace TwitterClone.Application.Services;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IUserRepository _userRepository;

    public NotificationService(INotificationRepository notificationRepository, IUserRepository userRepository)
    {
        _notificationRepository = notificationRepository;
        _userRepository = userRepository;
    }

    public NotificationDto? CreateNotification(CreateNotificationDto createNotificationDto)
    {
        if(_userRepository.GetUserById(createNotificationDto.UserId) == null)
        {
            return null;
        }
        if(createNotificationDto.TriggerByUserId.HasValue && _userRepository.GetUserById(createNotificationDto.TriggerByUserId.Value) == null)
        {
            return null;
        }
        var notification = BuildNotification(createNotificationDto);
        if(notification == null)
        {
            return null;
        }
        notification.UserId = createNotificationDto.UserId;
        var createdNotification = _notificationRepository.AddNotification(notification);
        return new NotificationDto
        {
            Id = createdNotification.Id,
            UserId = createdNotification.UserId,
            Message = createdNotification.GetMessage(),
            Type = createdNotification.Type,
            IsRead = createdNotification.IsRead,
            CreatedAt = createdNotification.CreatedAt,
            ModifiedAt = createdNotification.ModifiedAt
        };
    }

    public NotificationDto? GetNotificationById(Guid id)
    {
        var notification = _notificationRepository.GetNotificationById(id);
        return notification == null ? null : new NotificationDto
        {
            Id = notification.Id,
            UserId = notification.UserId,
            Message = notification.GetMessage(),
            Type = notification.Type,
            IsRead = notification.IsRead,
            CreatedAt = notification.CreatedAt,
            ModifiedAt = notification.ModifiedAt
        };
    }

    public List<NotificationDto> GetNotifications(Guid? userId, string? type, bool? isRead)
    {
        return new List<NotificationDto>(
            _notificationRepository.GetNotifications(userId, type, isRead)
                .OrderByDescending(notification => notification.CreatedAt)
            .Select(notification => new NotificationDto
            {
                Id = notification.Id,
                UserId = notification.UserId,
                Message = notification.GetMessage(),
                Type = notification.Type,
                IsRead = notification.IsRead,
                CreatedAt = notification.CreatedAt,
                ModifiedAt = notification.ModifiedAt
            })
        );
    }

    public NotificationDto? MarkNotificationAsRead(Guid id)
    {
        var notification = _notificationRepository.GetNotificationById(id);
        if(notification == null)
        {
            return null;
        }
        MarkAsRead(notification);
        return new NotificationDto
        {
            Id = notification.Id,
            UserId = notification.UserId,
            Message = notification.GetMessage(),
            Type = notification.Type,
            IsRead = notification.IsRead,
            CreatedAt = notification.CreatedAt,
            ModifiedAt = notification.ModifiedAt
        };
    }

    public int MarkAllNotificationsAsRead(Guid userId)
    {
        var unreadNotifications = _notificationRepository.GetNotifications(userId, null, false);
        foreach (var notification in unreadNotifications)
        {
            MarkAsRead(notification);
        }
        return unreadNotifications.Count;
    }

    public bool DeleteNotification(Guid id)
    {
        var notification = _notificationRepository.GetNotificationById(id);
        if(notification == null)
        {
            return false;
        }
        return _notificationRepository.DeleteNotification(notification);
    }

    private void MarkAsRead(Notification notification)
    {
        if (notification.IsRead)
        {
            return;
        }

        notification.IsRead = true;
        notification.MarkAsModified();

        _notificationRepository.UpdateNotification(notification);
    }

    private static Notification? BuildNotification(CreateNotificationDto createNotificationDto)
    {
        var triggeredByUserId = createNotificationDto.TriggeredByUserId ?? Guid.Empty;

        return createNotificationDto.Type switch
        {
            "Like" => new LikeNotification(triggeredByUserId),
            "Comment" => new CommentNotification(triggeredByUserId),
            "Mention" => new MentionNotification(triggeredByUserId),
            "FriendRequest" => new FriendRequestNotification(triggeredByUserId),
            "System" => new SystemNotification(),
            _ => null
        };
    }
}