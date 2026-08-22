namespace TwitterClone.Domain.Entities;

public class Tweet : BaseEntity, ILikeable
{
    public Guid AuthorId { get; set; }
    public string Content { get; set; }
    public static int MaxContentLength => 280;
    
    public Tweet(Guid authorId) : base(Guid.NewGuid())
    {
        AuthorId = authorId;
    }

    public void AddContent(string content)
    {
        Content = content;
    }

    public void AddContent(Guid userId, string content)
    {
        AuthorId = userId;
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
        return $"{baseRecord}, AuthorId: {AuthorId}, Content: {Content}";
    }
    
}