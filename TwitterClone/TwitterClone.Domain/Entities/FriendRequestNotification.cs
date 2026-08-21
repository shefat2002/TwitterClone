namespace TwitterClone.Domain.Entities;

public sealed class FriendRequestNotification : Notification
{
    public Guid RequestedByUserId { get; set; }

    public FriendRequestNotification(Guid requestedByUserId):base("FriendRequest")
    {
        RequestedByUserId = requestedByUserId;
    }


    public override string GetMessage()
    {
        return $"User {RequestedByUserId} sent you a friend request";
    }
}