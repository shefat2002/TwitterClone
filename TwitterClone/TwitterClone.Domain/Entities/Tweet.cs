namespace TwitterClone.Domain.Entities;

public class Tweet : BaseEntity
{
    public string AuthorId { get; set; }
    public string Content { get; set; }
    
    public Tweet(string authorId, string content) : base(Guid.NewGuid())
    {
        AuthorId = authorId;
        Content = content;
    }
}