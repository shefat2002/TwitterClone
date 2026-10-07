namespace TwitterClone.Application.Dtos.Tweet;

public class TweetDto
{
    public required Guid Id { get; set; }
    public required Guid UserId { get; set; }
    public required string Content { get; set; }
}