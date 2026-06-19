using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using WarehouseApi.Application.Categories;

namespace WarehouseApi.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);
        
        services.AddScoped<ICategoryService, CategoryService>();
        
        return services;
    }
}