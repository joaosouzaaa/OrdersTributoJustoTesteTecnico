using FluentValidation;
using FluentValidation.Results;
using OrdersTributoJustoTesteTecnico.Business.Interfaces.Validatation;

namespace OrdersTributoJustoTesteTecnico.Business.Settings.ValidationSettings
{
    public abstract class Validate<TEntity> : AbstractValidator<TEntity>, IValidate<TEntity>
        where TEntity : class
    {
        public async Task<ValidationResult> ValidateAsync(TEntity entity) =>
            await base.ValidateAsync(entity);
    }
}
