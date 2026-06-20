using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using WarehouseApi.Application.Categories;
using WarehouseApi.Application.Products;

namespace WarehouseApi.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);
        
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductService, ProductService>();
        
        return services;
    }
}