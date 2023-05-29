using ClothingStore.Domain.Interfaces;
using ClothingStore.Infrastructure.Data;
using ClothingStore.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ClothingStore.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        services.AddDbContextPool<Context>(builder =>
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            var connectionString = configuration.GetConnectionString("ClothingStoreDatabase");
            builder
                .UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention()
                .LogTo(Console.WriteLine, LogLevel.Information);
        });
        return services;
    }
}