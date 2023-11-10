using FluentAssertions;
using FluentValidation.Results;
using PaycheckChallenge.Application.Commands.CreateEmployee;
using PaycheckChallenge.Domain.Notifications;
using Xunit;

namespace PaycheckChallenge.Tests.Unit.Domain.Notifications;
public class NotificationContextTests
{
    private readonly NotificationContext _notificationContext;

    public NotificationContextTests()
    {
        _notificationContext = new NotificationContext();
    }

    [Fact]
    public void Should_return_false_when_not_exist_notifications()
    {
        var result = _notificationContext.HasNotifications();

        Assert.False(result);
    }

    [Fact]
    public void Should_return_true_when_exist_notifications()
    {
        _notificationContext.AddNotification("Key", "Message");

        var result = _notificationContext.HasNotifications();

        Assert.True(result);
    }

    [Fact]
    public void Should_add_notification_using_key_and_message()
    {
        var keyExpected = "key";
        var messageExpected = "Message";

        _notificationContext.AddNotification(keyExpected, messageExpected);

        _notificationContext.Notifications.Should().HaveCount(1);
        _notificationContext.Notifications.First().Key.Should().Be(keyExpected);
        _notificationContext.Notifications.First().Message.Should().Be(messageExpected);
    }

    [Fact]
    public void Should_add_notifications_using_validationResult()
    {
        var validationResult = BuildValidationResultInvalid();

        _notificationContext.AddNotifications(validationResult);

        _notificationContext.Notifications.Should().HaveCountGreaterThan(0);
    }

    private ValidationResult BuildValidationResultInvalid()
    {
        var command = new CreateEmployeeCommand();

        var validation = new CreateEmployeeCommandValidation().Validate(command);

        return validation;
    }
}
