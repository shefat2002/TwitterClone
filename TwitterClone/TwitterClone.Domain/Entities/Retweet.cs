namespace TwitterClone.Domain.Entities;

public class Retweet : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid TweetId { get; set; }
    public string? Comment { get; set; }
    
    public Retweet(Guid userId, Guid tweetId, string comment="") : base(Guid.NewGuid())
    {
        UserId = userId;
        TweetId = tweetId;
        Comment = comment;
    }
}