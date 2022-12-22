using CafeManager.Core.Entities;
using Microsoft.EntityFrameworkCore;

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
        base.OnModelCreating(modelBuilder);
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