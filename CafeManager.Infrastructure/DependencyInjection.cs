using CafeManager.Application.IRepositories;
using CafeManager.Infrastructure.EF;
using CafeManager.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace CafeManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        var connectionString = @"Server=localhost,1433;Database=Cafe;User Id=SA;Password=12345678;TrustServerCertificate=true;";
        services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));
        
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        
        return services;
    }
    
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services;
    }
}