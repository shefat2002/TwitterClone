namespace TwitterClone.Domain.Entities;

public class Message: BaseEntity
{
    public Guid SenderId { get; private set; }
    public Guid ReceiverId { get; private set; }
    public string? Content { get; private set; }
    public bool IsRead { get; set; }
    
    public Message (Guid senderId, Guid receiverId, string? content) : base(Guid.NewGuid())
    {
        SenderId = senderId;
        ReceiverId = receiverId;
        Content = content;
        IsRead = false;
    }
}