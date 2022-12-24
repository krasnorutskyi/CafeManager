using CafeManager.Infrastructure.EF;

namespace CafeManager.Infrastructure.DataInitializer;

public class DbInitializer
{
    public void Initialize(ApplicationContext context)
    {
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }
}