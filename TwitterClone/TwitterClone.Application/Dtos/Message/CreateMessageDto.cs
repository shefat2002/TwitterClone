using System.ComponentModel.DataAnnotations;

namespace TwitterClone.Application.Dtos.Message;

public class CreateMessageDto
{
    public required Guid SenderId { get; set; }
    public required Guid ReceiverId { get; set; }
    [Required]
    [StringLength(1000, MinimumLength = 1, ErrorMessage = "Content must be between 1 and 280 characters.")]
    public required string Content { get; set; }
}