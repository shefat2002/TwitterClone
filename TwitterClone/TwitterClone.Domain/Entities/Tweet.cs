namespace TwitterClone.Domain.Entities;

public class Tweet
{
    private Guid _id;
    private Guid _authorId;
    private string _content;

    public Guid Id
    {
        get {return _id;}
    }
    public Guid AuthorId
    {
        get {return _authorId;}
    }

    public string Content
    {
        get {return _content;}
        set {_content = value;}
    }
    private Tweet()
    {
    }

    public Tweet(Guid id, Guid authorId, string content)
    {
        _id = id;
        _authorId = authorId;
        _content = content;
    }
}