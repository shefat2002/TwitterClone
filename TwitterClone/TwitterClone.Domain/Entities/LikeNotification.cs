namespace TwitterClone.Domain.Entities;

public class LikeNotification : Notification
{
    public Guid LikebyUserId { get; private set; }
    
    public LikeNotification(Guid likebyUserId, Guid tweetId) : base("Like")
    {
        LikebyUserId = likebyUserId;
    }
    public override string GetMessage()
    {
        return $"User {LikebyUserId} liked your tweet";
    }
    public void AddMessage(string message)
    {
        Message = message;
    }
    public override string DescribeRecord()
    {
        var baseRecord = base.DescribeRecord();
        return $"{baseRecord}, LikebyUserId: {LikebyUserId}";
    }
}