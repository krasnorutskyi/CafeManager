using CafeManager.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CafeManager.Infrastructure.FluentAPI;

public class DishesOrdersEntityConfiguration : IEntityTypeConfiguration<DishesOrders>
{
    public void Configure(EntityTypeBuilder<DishesOrders> builder)
    {
        builder.HasKey(dp => new {dp.DishId, dp.OrdersNumber});

        builder.HasOne<Dish>(dp => dp.Dish)
            .WithMany(d => d.DishesOrders)
            .HasForeignKey(dp => dp.DishId);

        builder.HasOne<Order>(dp => dp.Order)
            .WithMany(p => p.DishesOrders)
            .HasForeignKey(dp => dp.OrdersNumber);
    }
}