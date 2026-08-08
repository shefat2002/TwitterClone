namespace TwitterClone.Domain.Entities;

public class Notification
{
    private Guid _id;
    private Guid _userId;
    private string _message;
    private DateTime _createdAt;

    public Guid Id
    {
        get { return _id; }
    }
    public Guid UserId
    {
        get { return _userId; }
    }
    public string Message
    {
        get { return _message; }
        set { _message = value; }
    }
    public DateTime CreatedAt
    {
        get { return _createdAt; }
    }
}