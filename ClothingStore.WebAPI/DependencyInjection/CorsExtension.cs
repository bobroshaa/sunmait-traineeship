using ClothingStore.WebAPI.Configuration;

namespace ClothingStore.WebAPI.DependencyInjection;

public static class CorsExtension
{
    public static IServiceCollection AddCors(
        this IServiceCollection services,
        CorsPolicyConfiguration corsPolicyConfiguration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(corsPolicyConfiguration.Name, corsPolicyBuilder =>
            {
                corsPolicyBuilder.WithOrigins(corsPolicyConfiguration.Origins)
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        return services;
    }
}