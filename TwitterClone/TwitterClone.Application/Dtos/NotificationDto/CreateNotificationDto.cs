namespace TwitterClone.Application.Dtos.NotificationDto;

public class CreateNotificationDto
{
    public required Guid UserId { get; set; }
    public required string Type { get; set; }
    public Guid? TriggerByUserId { get; set; }
}