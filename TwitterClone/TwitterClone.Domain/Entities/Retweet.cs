namespace TwitterClone.Domain.Entities;

public class Retweet
{
    private Guid _id;
    private Guid _userId;
    private Guid _originalTweetId;
    private DateTime _retweetedAt;

    public Guid Id
    {
        get { return _id; }
    }
    public Guid UserId
    {
        get { return _userId; }
    }
    public Guid OriginalTweetId
    {
        get { return _originalTweetId; }
    }
    public DateTime RetweetedAt
    {
        get { return _retweetedAt; }
    }
}