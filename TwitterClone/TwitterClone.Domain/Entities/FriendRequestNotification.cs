namespace TwitterClone.Domain.Entities;

public sealed class FriendRequestNotification : Notification
{
    public Guid RequestedByUserId { get; set; }

    public FriendRequestNotification(Guid requestedByUserId):base("FriendRequest")
    {
        RequestedByUserId = requestedByUserId;
    }

    public void AddMessage(string message)
    {
        Message = message;
    }

    public override string GetMessage()
    {
        return $"User {RequestedByUserId} sent you a friend request";
    }

    public override string DescribeRecord()
    {
        var baseRecord = base.DescribeRecord();
        return $"{baseRecord}, RequestedByUserId: {RequestedByUserId}";
    }
}