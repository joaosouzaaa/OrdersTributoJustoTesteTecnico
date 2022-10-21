using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using OrdersTributoJustoTesteTecnico.Domain.Entities.EntitiesBase;
using OrdersTributoJustoTesteTecnico.Infra.Contexts;
using OrdersTributoJustoTesteTecnico.Business.Settings.PaginationSettings;
using OrdersTributoJustoTesteTecnico.Business.Interfaces.Pagination;
using OrdersTributoJustoTesteTecnico.Business.Interfaces.Repositories.BaseRepositories;

namespace OrdersTributoJustoTesteTecnico.Infra.Repositories.BaseRepositories
{
    public abstract class BaseQueryCommandsRepository<TEntity> : BaseCommandsRepository<TEntity>, IBaseQueryCommandsRepository<TEntity>
        where TEntity : BaseEntity
    {
        private readonly IPaginationService<TEntity> _paginationService;

        protected BaseQueryCommandsRepository(IPaginationService<TEntity> paginationService, OrdersTributoJustoTesteTecnicoDbContext dbContext) : base(dbContext)
        {
            _paginationService = paginationService;
        }

        public virtual async Task<TEntity> GetByIdAsync(int id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include, bool asNoTracking)
        {
            var query = (IQueryable<TEntity>)_dbContext.Set<TEntity>();

            if (asNoTracking)
                query = _dbContextSet.AsNoTracking();

            if (include != null)
                query = include(query);

            return await query.FirstOrDefaultAsync(e => e.Id == id);
        }

        public virtual async Task<List<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include)
        {
            var query = _dbContextSet.AsNoTracking();

            if (include != null)
                query = include(query);

            return await query.ToListAsync();
        }

        public virtual async Task<PageList<TEntity>> GetAllWithPaginationAsync(PageParams pageParams, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include)
        {
            var query = _dbContextSet.AsNoTracking();

            if (include != null)
                query = include(query);

            return await _paginationService.CreatePaginationAsync(query, pageParams.pageNumber, pageParams.pageSize);
        }
    }
}
