namespace TwitterClone.Domain.Entities;

public interface INotifiable
{
    void AddNotification(Notification notification);
}