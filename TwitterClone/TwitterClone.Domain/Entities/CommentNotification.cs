namespace TwitterClone.Domain.Entities;

public class CommentNotification : Notification
{
    public Guid CommentByUserId { get; set; }
    
    public CommentNotification(Guid commentByUserId) : base("Comment")
    {
        CommentByUserId = commentByUserId;
    }
    public void AddMessage(string message)
    {
        Message = message;
    }

    public override string DescribeRecord()
    {
        var baseRecord = base.DescribeRecord();
        return $"{baseRecord}, CommentByUserId: {CommentByUserId}";
    }

    public override string GetMessage()
    {
        return $"User {CommentByUserId} commented on your tweet";
    }
}