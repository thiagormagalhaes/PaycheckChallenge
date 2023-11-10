using FluentValidation.Results;
using PaycheckChallenge.Domain.Interfaces;

namespace PaycheckChallenge.Domain.Notifications;

public class NotificationContext : INotificationContext
{
    private readonly List<Notification> _notifications;
    public IReadOnlyCollection<Notification> Notifications => _notifications;

    public List<Notification> GetNotifications() => _notifications;

    public bool HasNotifications() => _notifications.Any();

    public NotificationContext()
    {
        _notifications = new();
    }

    public void AddNotification(string key, string message)
    {
        _notifications.Add(new Notification(key, message));
    }

    public void AddNotifications(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            AddNotification(error.ErrorCode, error.ErrorMessage);
        }
    }
}
