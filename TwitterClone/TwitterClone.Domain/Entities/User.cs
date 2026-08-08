namespace TwitterClone.Domain.Entities;

public class User
{
    private Guid _id;
    private string _username;
    private string _email;

    public Guid Id
    {
        get { return _id; }
    }

    public string Username
    {
        get { return _username; }
    }
    public string Email
    {
        get { return _email; }
    }

    private User()
    {
    }
    public User(Guid id, string username, string email)
    {
        _id = id;
        _username = username;
        _email = email;
    }
}