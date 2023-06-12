using ClothingStore.Domain.Enums;

namespace ClothingStore.WebAPI.DependencyInjection;

public static class AuthorizationExtension
{
    public static IServiceCollection AddCustomAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(opts =>
        {
            opts.AddPolicy("AdminAccess", policy =>
            {
                policy.RequireRole(Enum.GetName(Role.Admin));
            });
            opts.AddPolicy("CustomerAccess", policy =>
            {
                policy.RequireRole(Enum.GetName(Role.Admin), Enum.GetName(Role.Customer));
            });
        });

        return services;
    }
}