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
    public void AddMessage(string message)
    {
        Message = message;
    }
}