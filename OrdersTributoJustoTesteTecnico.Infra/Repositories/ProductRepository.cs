using OrdersTributoJustoTesteTecnico.Business.Interfaces.Pagination;
using OrdersTributoJustoTesteTecnico.Business.Interfaces.Repositories;
using OrdersTributoJustoTesteTecnico.Domain.Entities;
using OrdersTributoJustoTesteTecnico.Infra.Contexts;
using OrdersTributoJustoTesteTecnico.Infra.Repositories.BaseRepositories;

namespace OrdersTributoJustoTesteTecnico.Infra.Repositories
{
    public sealed class ProductRepository : BaseQueryCommandsRepository<Product>, IProductRepository
    {
        public ProductRepository(IPaginationService<Product> paginationService, OrdersTributoJustoTesteTecnicoDbContext dbContext) : base(paginationService, dbContext)
        {
        }
    }
}
