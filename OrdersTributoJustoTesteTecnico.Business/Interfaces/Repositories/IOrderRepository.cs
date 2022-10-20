using OrdersTributoJustoTesteTecnico.Business.Interfaces.Repositories.BaseRepositories;
using OrdersTributoJustoTesteTecnico.Domain.Entities;

namespace OrdersTributoJustoTesteTecnico.Business.Interfaces.Repositories
{
    public interface IOrderRepository : IBaseQueryCommandsRepository<Order>
    {
    }
}
