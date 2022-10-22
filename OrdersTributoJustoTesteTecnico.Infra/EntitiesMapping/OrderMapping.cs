using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrdersTributoJustoTesteTecnico.Domain.Entities;

namespace OrdersTributoJustoTesteTecnico.Infra.EntitiesMapping
{
    public sealed class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Quantity).HasColumnType("int")
                .HasColumnName("quantity").IsRequired(true);

            builder.Property(o => o.TotalPrice).HasColumnType("decimal(18, 2)")
                .HasColumnName("total_price").IsRequired(true);

            builder.Property(c => c.RegistrationDate).HasColumnType("datetime")
                .HasColumnName("RegistrationDate").IsRequired(true);

            builder.HasOne(o => o.Client)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(o => o.Products).WithMany(p => p.Order)
                .UsingEntity<Dictionary<string, object>>("OrderProduct", config =>
                {
                    config.HasOne<Order>().WithMany().HasForeignKey("OrderId");
                    config.HasOne<Product>().WithMany().HasForeignKey("ProductId");
                });
        }
    }
}
