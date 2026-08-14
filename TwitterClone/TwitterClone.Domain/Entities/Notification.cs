namespace TwitterClone.Domain.Entities;

public class Notification:BaseEntity
{
    public Guid UserId { get; set; }
    public string Type { get; set; }
    protected string Message { get; set; }
    public bool IsRead { get; set; }
    
    public Notification(string type, string message, Guid userId) : base(Guid.NewGuid())
    {
        Type = type;
        Message = message;
        UserId = userId;
        IsRead = false;
    }
}