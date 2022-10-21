using Microsoft.EntityFrameworkCore.Query;
using OrdersTributoJustoTesteTecnico.Business.Settings.PaginationSettings;
using OrdersTributoJustoTesteTecnico.Domain.Entities.EntitiesBase;

namespace OrdersTributoJustoTesteTecnico.Business.Interfaces.Repositories.BaseRepositories
{
    public interface IBaseQueryCommandsRepository<TEntity> : IBaseCommandsRepository<TEntity>
        where TEntity : BaseEntity
    {
        Task<TEntity> GetByIdAsync(int id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool asNoTracking = false);
        Task<List<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);
        Task<PageList<TEntity>> GetAllWithPaginationAsync(PageParams pageParams, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);
    }
}
