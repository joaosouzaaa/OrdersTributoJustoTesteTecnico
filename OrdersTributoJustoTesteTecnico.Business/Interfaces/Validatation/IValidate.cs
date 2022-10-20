using FluentValidation.Results;

namespace OrdersTributoJustoTesteTecnico.Business.Interfaces.Validatation
{
    public interface IValidate<TEntity>
        where TEntity : class
    {
        Task<ValidationResult> ValidateAsync(TEntity entity);
    }
}
