namespace TwitterClone.Domain.Entities;

public class Retweet : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid TweetId { get; set; }
    public string? Comment { get; set; }
    
    public Retweet() : base(Guid.NewGuid())
    {
    }

    public override string DescribeRecord()
    {
        var baseRecord = base.DescribeRecord();
        return $"{baseRecord}, UserId: {UserId}, TweetId: {TweetId}, Comment: {Comment}";
    }
}