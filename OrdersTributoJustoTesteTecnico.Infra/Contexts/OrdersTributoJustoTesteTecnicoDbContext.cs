using Microsoft.EntityFrameworkCore;
using OrdersTributoJustoTesteTecnico.Domain.Entities;

namespace OrdersTributoJustoTesteTecnico.Infra.Contexts
{
    public sealed class OrdersTributoJustoTesteTecnicoDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Product> Products { get; set; }

        public OrdersTributoJustoTesteTecnicoDbContext(DbContextOptions<OrdersTributoJustoTesteTecnicoDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrdersTributoJustoTesteTecnicoDbContext).Assembly);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType()
                    .GetProperty("RegistrationDate") != null))
            {
                if (entry.State == EntityState.Added)
                    entry.Property("RegistrationDate").CurrentValue = DateTime.Now;

                else if (entry.State == EntityState.Modified)
                    entry.Property("RegistrationDate").IsModified = false;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
