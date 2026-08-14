namespace TwitterClone.Domain.Entities;

public class Bookmark : BaseEntity
{
    public Guid UserId { get; private set; }
    public Guid TweetId { get; private set; }

    public Bookmark(Guid userId, Guid tweetId):base(Guid.NewGuid())
    {
        UserId = userId;
        TweetId = tweetId;
    }
}