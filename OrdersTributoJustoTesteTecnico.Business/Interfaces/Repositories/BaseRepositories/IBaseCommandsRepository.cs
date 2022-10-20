using OrdersTributoJustoTesteTecnico.Domain.Entities.EntitiesBase;
using System.Linq.Expressions;

namespace OrdersTributoJustoTesteTecnico.Business.Interfaces.Repositories.BaseRepositories
{
    public interface IBaseCommandsRepository<TEntity>
        where TEntity : BaseEntity
    {
        Task<bool> AddAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> HaveObjectInDbAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
