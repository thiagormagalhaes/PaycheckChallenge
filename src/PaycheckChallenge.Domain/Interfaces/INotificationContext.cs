using FluentValidation.Results;
using PaycheckChallenge.Domain.Notifications;

namespace PaycheckChallenge.Domain.Interfaces;

public interface INotificationContext
{
    List<Notification> GetNotifications();
    bool HasNotifications();
    void AddNotification(string key, string message);
    void AddNotifications(ValidationResult validationResult);
}
