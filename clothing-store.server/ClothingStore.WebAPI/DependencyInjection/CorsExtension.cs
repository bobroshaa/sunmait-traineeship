using ClothingStore.WebAPI.Configuration;

namespace ClothingStore.WebAPI.DependencyInjection;

public static class CorsExtension
{
    public static IServiceCollection AddCustomCors(
        this IServiceCollection services,
        CorsPolicyConfiguration corsPolicyConfiguration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(corsPolicyConfiguration.Name, corsPolicyBuilder =>
            {
                corsPolicyBuilder.WithOrigins(corsPolicyConfiguration.Origins)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        return services;
    }
}