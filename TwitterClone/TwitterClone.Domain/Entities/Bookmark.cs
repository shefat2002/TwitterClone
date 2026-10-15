namespace TwitterClone.Domain.Entities;

public class Bookmark : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid TweetId { get; set; }

    public Bookmark(Guid userId, Guid tweetId):base(Guid.NewGuid())
    {
        UserId = userId;
        TweetId = tweetId;
    }

    public Bookmark() : base(Guid.NewGuid())
    {
    }

    public override string DescribeRecord()
    {
        var baseRecord = base.DescribeRecord();
        return $"{baseRecord}, UserId: {UserId}, TweetId: {TweetId}";
    }
}