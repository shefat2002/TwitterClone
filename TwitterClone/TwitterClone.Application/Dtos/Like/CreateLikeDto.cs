namespace TwitterClone.Application.Dtos.Like;

public class CreateLikeDto
{
    public required Guid UserId { get; set; }
    public required Guid TweetId { get; set; }
}