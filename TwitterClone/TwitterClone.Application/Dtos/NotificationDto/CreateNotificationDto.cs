using System.ComponentModel.DataAnnotations;

namespace TwitterClone.Application.Dtos.NotificationDto;

public class CreateNotificationDto
{
    public required Guid UserId { get; set; }
    public required string Type { get; set; }
    public Guid? TriggerByUserId { get; set; }
    public Guid? TriggeredByUserId { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if(Type != "System" && TriggerByUserId == null)
        {
            yield return new ValidationResult(
                "TriggerByUserId is required for non-system notifications.", 
                new[] { nameof(TriggerByUserId) });
        }
    }
}