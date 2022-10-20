using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrdersTributoJustoTesteTecnico.Domain.Entities;

namespace OrdersTributoJustoTesteTecnico.Infra.EntitiesMapping
{
    public sealed class ClientMapping : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.FirstName).HasColumnType("varchar(50)")
                .HasColumnName("first_name").IsRequired(true);

            builder.Property(c => c.LastName).HasColumnType("varchar(50)")
                .HasColumnName("last_name").IsRequired(true);

            builder.Property(c => c.Cpf).HasColumnType("char(11)")
                .HasColumnName("cpf").IsRequired(true);

            builder.Property(c => c.Age).HasColumnType("int")
                .HasColumnName("age").IsRequired(true);

            builder.Property(c => c.RegistrationDate).HasColumnType("datetime")
                .HasColumnName("RegistrationDate").IsRequired(true);
        }
    }
}
