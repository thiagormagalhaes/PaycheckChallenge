using Microsoft.Extensions.DependencyInjection;
using PaycheckChallenge.Domain.Discounts;
using PaycheckChallenge.Domain.Interfaces;
using PaycheckChallenge.Domain.Interfaces.Services;
using PaycheckChallenge.Domain.Notifications;
using PaycheckChallenge.Domain.Services;
using PaycheckChallenge.Domain.Validations;

namespace PaycheckChallenge.Domain.Configurations;

public static class DependencyInjection
{
    public static void AddDomain(IServiceCollection services)
    {
        services.AddScoped<INotificationContext, NotificationContext>();
        services.AddScoped<IDiscountService, DiscountService>();
        services.AddScoped<IDiscount, DentalPlanDiscount>();
        services.AddScoped<IDiscount, FgtsDiscount>();
        services.AddScoped<IDiscount, HealthPlanDiscount>();
        services.AddScoped<IDiscount, IncomeTaxDiscount>();
        services.AddScoped<IDiscount, InssDiscount>();
        services.AddScoped<IDiscount, TransportVoucherDiscount>();
        services.AddSingleton<IDiscountRulesService, DiscountRulesService>();
        services.AddScoped<ICpfValidator, CpfValidator>();
    }
}
