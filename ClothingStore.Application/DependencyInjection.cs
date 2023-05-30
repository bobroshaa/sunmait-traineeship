using ClothingStore.Application.Interfaces;
using ClothingStore.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ClothingStore.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddTransient<IBrandService, BrandService>();
        services.AddTransient<IProductService, ProductService>();
        services.AddTransient<IOrderService, OrderService>();
        services.AddTransient<ICategoryService, CategoryService>();
        services.AddTransient<ISectionService, SectionService>();
        services.AddTransient<IUserService, UserService>();

        return services;
    }
}