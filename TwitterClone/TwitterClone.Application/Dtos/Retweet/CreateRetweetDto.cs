namespace TwitterClone.Application.Dtos.Retweet;

public class CreateRetweetDto
{
    public required Guid UserId { get; set; }
    public required Guid TweetId { get; set; }
    public string? Comment { get; set; }
}