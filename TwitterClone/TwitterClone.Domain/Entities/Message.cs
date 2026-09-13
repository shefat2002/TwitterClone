namespace TwitterClone.Domain.Entities;

public class Message: BaseEntity
{
    public Guid SenderId { get; private set; }
    public Guid ReceiverId { get; private set; }
    public string? Content { get; private set; }
    public bool IsRead { get; set; }
    
    public Message () : base(Guid.NewGuid())
    {
    }

    public override string DescribeRecord()
    {
        var baseRecord = base.DescribeRecord();
        return $"{baseRecord}, SenderId: {SenderId}, ReceiverId: {ReceiverId}, Content: {Content}, IsRead: {IsRead}";
    }
}