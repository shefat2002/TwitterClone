namespace TwitterClone.Domain.Entities;

public class User : BaseEntity, IFollowable, INotifiable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
   
    public User(string firstName, string lastName, string email) : base(Guid.NewGuid())
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }
    private List<Guid> _followers = new List<Guid>();
    private List<Guid> _incomingNotifications = new List<Guid>();
    
    public override string DescribeRecord()
    {
        var baseRecord = base.DescribeRecord();
        return $"{baseRecord} - UserId: {Id}, FirstName: {FirstName}, LastName: {LastName}, Email: {Email}";
    }

    public void Follow(Guid userId)
    {
        if (!_followers.Contains(userId))
        {
            _followers.Add(userId);
        }
    }

    public void Unfollow(Guid userId)
    {
        if (_followers.Contains(userId))
        {
            _followers.Remove(userId);
        }
    }

    public void AddNotification(Notification notification)
    {
        if (!_incomingNotifications.Contains(notification.Id))
        {
            _incomingNotifications.Add(notification.Id);
        }
    }
}