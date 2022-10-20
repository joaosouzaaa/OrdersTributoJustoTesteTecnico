using Microsoft.EntityFrameworkCore;
using OrdersTributoJustoTesteTecnico.Business.Interfaces.Repositories.BaseRepositories;
using OrdersTributoJustoTesteTecnico.Domain.Entities.EntitiesBase;
using OrdersTributoJustoTesteTecnico.Infra.Contexts;
using System.Linq.Expressions;

namespace OrdersTributoJustoTesteTecnico.Infra.Repositories.BaseRepositories
{
    public abstract class BaseCommandsRepository<TEntity> : IBaseCommandsRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected OrdersTributoJustoTesteTecnicoDbContext _dbContext;
        protected DbSet<TEntity> _dbContextSet => _dbContext.Set<TEntity>();

        public BaseCommandsRepository(OrdersTributoJustoTesteTecnicoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private async Task<bool> SaveChangesAsync() => await _dbContext.SaveChangesAsync() > 0;

        public virtual async Task<bool> AddAsync(TEntity entity)
        {
            await _dbContextSet.AddAsync(entity);

            return await SaveChangesAsync();
        }

        public virtual async Task<bool> UpdateAsync(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;

            return await SaveChangesAsync();
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbContextSet.FirstOrDefaultAsync(e => e.Id == id);

            _dbContextSet.Remove(entity);

            return await SaveChangesAsync();
        }

        public virtual async Task<bool> HaveObjectInDbAsync(Expression<Func<TEntity, bool>> predicate) => await _dbContextSet.AnyAsync(predicate);
    }
}
