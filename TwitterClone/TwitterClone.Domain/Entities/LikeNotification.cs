namespace TwitterClone.Domain.Entities;

public class LikeNotification : Notification
{
    public Guid LikebyUserId { get; private set; }
    public Guid TweetId { get; private set; }
    
    public LikeNotification(Guid likebyUserId, Guid tweetId) : base("Like")
    {
        LikebyUserId = likebyUserId;
        TweetId = tweetId;
    }
    public override string GetMessage()
    {
        return $"User {LikebyUserId} liked your tweet";
    }
}