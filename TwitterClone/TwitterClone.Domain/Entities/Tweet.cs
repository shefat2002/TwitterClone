namespace TwitterClone.Domain.Entities;

public class Tweet : BaseEntity
{
    public Guid AuthorId { get; set; }
    public string Content { get; set; }
    
    public Tweet(Guid authorId, string content) : base(Guid.NewGuid())
    {
        AuthorId = authorId;
        Content = content;
    }
}