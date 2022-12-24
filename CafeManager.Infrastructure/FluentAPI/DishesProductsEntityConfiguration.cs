using CafeManager.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CafeManager.Infrastructure.FluentAPI;

public class DishesProductsEntityConfiguration : IEntityTypeConfiguration<DishesProducts>
{
    public void Configure(EntityTypeBuilder<DishesProducts> builder)
    {
        builder.HasKey(dp => new {dp.DishId, dp.ProductId});

        builder.HasOne<Dish>(dp => dp.Dish)
            .WithMany(d => d.DishesProducts)
            .HasForeignKey(dp => dp.DishId);

        builder.HasOne<Product>(dp => dp.Product)
            .WithMany(p => p.DishesProducts)
            .HasForeignKey(dp => dp.ProductId);
    }
}