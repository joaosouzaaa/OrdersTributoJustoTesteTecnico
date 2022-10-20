using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrdersTributoJustoTesteTecnico.Domain.Entities;

namespace OrdersTributoJustoTesteTecnico.Infra.EntitiesMapping
{
    public sealed class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).HasColumnType("varchar(100)")
                .HasColumnName("name").IsRequired(true);

            builder.Property(p => p.Price).HasColumnType("decimal(18, 2)")
                .HasColumnName("price").IsRequired(true);

            builder.Property(p => p.Image).HasColumnType("longtext")
                .HasColumnName("image").IsRequired(false);
            
            builder.Property(c => c.RegistrationDate).HasColumnType("datetime")
                .HasColumnName("RegistrationDate").IsRequired(true);
        }
    }
}
