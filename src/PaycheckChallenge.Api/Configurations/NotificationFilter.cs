using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using PaycheckChallenge.Domain.Interfaces;
using System.Net;

namespace PaycheckChallenge.Api.Configurations;

public class NotificationFilter : IAsyncResultFilter
{
    private readonly INotificationContext _notificationContext;

    public NotificationFilter(INotificationContext notificationContext)
    {
        _notificationContext = notificationContext;
    }

    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (_notificationContext.HasNotifications())
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.HttpContext.Response.ContentType = "application/json";

            var notifications = JsonConvert.SerializeObject(_notificationContext.GetNotifications());
            await context.HttpContext.Response.WriteAsync(notifications);

            return;
        }

        await next();
    }
}