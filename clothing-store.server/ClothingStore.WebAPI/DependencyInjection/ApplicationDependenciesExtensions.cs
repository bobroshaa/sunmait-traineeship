using ClothingStore.Application.Interfaces;
using ClothingStore.Application.Services;

namespace ClothingStore.WebAPI.DependencyInjection;

public static class ApplicationDependenciesExtensions
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddTransient<IBrandService, BrandService>();
        services.AddTransient<IProductService, ProductService>();
        services.AddTransient<IOrderService, OrderService>();
        services.AddTransient<ICategoryService, CategoryService>();
        services.AddTransient<ISectionService, SectionService>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IReviewService, ReviewService>();
        services.AddTransient<ICartService, CartService>();

        return services;
    }
}