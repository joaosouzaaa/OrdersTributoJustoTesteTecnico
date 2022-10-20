using OrdersTributoJustoTesteTecnico.Business.Settings.PaginationSettings;

namespace OrdersTributoJustoTesteTecnico.Business.Interfaces.Pagination
{
    public interface IPaginationService<TEntity>
        where TEntity : class
    {
        Task<PageList<TEntity>> CreatePaginationAsync(IQueryable<TEntity> source, int pageNumber, int pageSize);
    }
}
