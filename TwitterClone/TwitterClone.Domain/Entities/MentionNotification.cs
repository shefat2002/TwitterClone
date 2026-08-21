namespace TwitterClone.Domain.Entities;

public class MentionNotification : Notification
{
    public Guid MentionedByUserId { get; private set; }
    public MentionNotification(Guid mentionedByUserId) : base("Mention")
    {
        MentionedByUserId = mentionedByUserId;
    }
    
    public override string GetMessage()
    {
        return $"User {MentionedByUserId} mentioned you in a tweet";
    }
}