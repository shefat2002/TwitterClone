namespace TwitterClone.Domain.Entities;

public class Tweet : BaseEntity, ILikeable
{
    public Guid UserId { get; set; }
    public string Content { get; set; }
    public static int MaxContentLength => 200;
    
    public Tweet(string content) : base(Guid.NewGuid())
    {
        Content = content;
    }
    public Tweet(Guid userId, string content) : base(Guid.NewGuid())
    {
        UserId = userId;
        Content = content;
    }

    public void AddContent(string content)
    {
        Content = content;
    }

    public void AddContent(Guid userId, string content)
    {
        UserId = userId;
        Content = content;
    }
    public bool CanBeLiked()
    {
        if(string.IsNullOrEmpty(Content))
        {
            return false;
        }
        return true;
    }
    public override string DescribeRecord()
    {
        var baseRecord = base.DescribeRecord();
        return $"{baseRecord}, UserId: {UserId}, Content: {Content}";
    }
    
}