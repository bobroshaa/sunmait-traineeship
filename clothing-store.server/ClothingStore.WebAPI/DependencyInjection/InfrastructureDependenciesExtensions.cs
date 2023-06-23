using ClothingStore.Domain.Interfaces;
using ClothingStore.Infrastructure.Data;
using ClothingStore.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.WebAPI.DependencyInjection;

public static class InfrastructureDependenciesExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ISectionRepository, SectionRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();

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