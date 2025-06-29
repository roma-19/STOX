using STOX.Repo.Interfaces;
using STOX.Repo.Repositories;
using STOX.Service.Interfaces;
using STOX.Service.Services;

namespace STOX.Web;

public static class Initializer
{
    public static void InitializeRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUserRepository, UserRepository>();
    }
    public static void InitializeServices(this IServiceCollection services)
    {
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<ICartItemService, CartItemService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IOrderItemService, OrderItemService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IReviewService, ReviewService>();
        services.AddScoped<IUserService, UserService>();
    }
}