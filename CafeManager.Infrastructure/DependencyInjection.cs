using CafeManager.Application.IRepositories;
using CafeManager.Application.IServices;
using CafeManager.Infrastructure.EF;
using CafeManager.Infrastructure.Repositories;
using CafeManager.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CafeManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        var connectionString = @"Server=localhost,1433;Database=Cafe;User Id=SA;Password=12345678;TrustServerCertificate=true;";
        services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Transient);
        
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IDishRepository, DishRepository>();
        services.AddScoped<IStatisticsRepository, StatisticsRepository>();
        return services;
    }
    
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IDishService, DishService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ITableService, TableService>();
        services.AddScoped<IUnitService, UnitService>();
        services.AddScoped<IWaiterService, WaiterService>();
        services.AddScoped<IStatisticsService, StatisticsService>();
        
        return services;
    }
}