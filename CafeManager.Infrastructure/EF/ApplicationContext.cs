using CafeManager.Core.Entities;
using Microsoft.EntityFrameworkCore;
using CafeManager.Infrastructure.FluentAPI;

namespace CafeManager.Infrastructure.EF;

public class ApplicationContext : DbContext
{
    public ApplicationContext()
    {
    }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DishesOrdersEntityConfiguration());
        modelBuilder.ApplyConfiguration(new DishesProductsEntityConfiguration());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = @"Server=localhost,1433;Database=Cafe;User Id=SA;Password=12345678;TrustServerCertificate=true;";
        optionsBuilder.UseSqlServer(connectionString);
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Dish> Dishes { get; set; }
    public DbSet<DishesOrders> DishesOrders { get; set; }
    public DbSet<DishesProducts> DishesProducts { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Table> Tables { get; set; }
    public DbSet<Unit> Units { get; set; }
    public DbSet<Waiter> Waiters { get; set; }
}