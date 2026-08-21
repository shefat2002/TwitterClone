namespace TwitterClone.Domain.Entities;

public class CommentNotification : Notification
{
    public Guid CommentByUserId { get; private set; }
    public Guid CommentedTweetId { get; private set; }
    
    public CommentNotification(Guid commentByUserId, Guid commentedTweetId) : base("Comment")
    {
        CommentByUserId = commentByUserId;
        CommentedTweetId = commentedTweetId;
    }
    public override string GetMessage()
    {
        return $"User {CommentByUserId} commented on your tweet";
    }
}