using Training.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<IProjectService, ProjectService>();
        services.AddSingleton<ICustomerService, CustomerService>();
        services.AddSingleton<IShippingService, ShippingService>();
        services.AddSingleton<IProductsService, ProductsService>();
        services.AddSingleton<ICartsService, CartsService>();
        services.AddSingleton<IOrdersService, OrdersService>();
        services.AddSingleton<IExtensionsService, ExtensionsService>();
        services.AddSingleton<IImportsService, ImportsService>();
        services.AddSingleton<IGraphQlService, GraphQlService>();
        
        return services;
    }
}
