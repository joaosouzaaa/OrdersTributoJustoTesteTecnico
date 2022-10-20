using Microsoft.EntityFrameworkCore;
using OrdersTributoJustoTesteTecnico.Business.Interfaces.Pagination;
using OrdersTributoJustoTesteTecnico.Business.Settings.PaginationSettings;

namespace OrdersTributoJustoTesteTecnico.Infra.Services
{
    public sealed class PaginationService<TEntity> : IPaginationService<TEntity>
        where TEntity : class
    {
        public async Task<PageList<TEntity>> CreatePaginationAsync(IQueryable<TEntity> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PageList<TEntity>(items, count, pageNumber, pageSize);
        }
    }
}
