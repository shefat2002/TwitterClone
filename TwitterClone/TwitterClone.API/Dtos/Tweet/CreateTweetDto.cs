namespace TwitterClone.API.Dtos.Tweet;

public class CreateTweetDto
{
    public required Guid UserId { get; set; }
    public required string Content { get; set; } = null!;
}