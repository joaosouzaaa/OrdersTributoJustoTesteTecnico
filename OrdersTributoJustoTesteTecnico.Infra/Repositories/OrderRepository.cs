using Microsoft.EntityFrameworkCore;
using OrdersTributoJustoTesteTecnico.Business.Interfaces.Pagination;
using OrdersTributoJustoTesteTecnico.Business.Interfaces.Repositories;
using OrdersTributoJustoTesteTecnico.Domain.Entities;
using OrdersTributoJustoTesteTecnico.Infra.Contexts;
using OrdersTributoJustoTesteTecnico.Infra.Repositories.BaseRepositories;

namespace OrdersTributoJustoTesteTecnico.Infra.Repositories
{
    public sealed class OrderRepository : BaseQueryCommandsRepository<Order>, IOrderRepository
    {
        public OrderRepository(IPaginationService<Order> paginationService, OrdersTributoJustoTesteTecnicoDbContext dbContext) : base(paginationService, dbContext)
        {
        }

        public override Task<bool> AddAsync(Order entity)
        {
            DetachProducts(entity.Products);

            return base.AddAsync(entity);
        }

        public override Task<bool> UpdateAsync(Order entity)
        {
            DetachProducts(entity.Products);

            return base.UpdateAsync(entity);
        }

        private void DetachProducts(List<Product> productList)
        {
            foreach (var product in productList)
            {
                _dbContext.Entry(product).State = EntityState.Unchanged;
            }
        }
    }
}
