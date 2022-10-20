using OrdersTributoJustoTesteTecnico.Business.Interfaces.Notification;
using OrdersTributoJustoTesteTecnico.Business.Interfaces.Validatation;

namespace OrdersTributoJustoTesteTecnico.ApplicationService.Services.BaseServices
{
    public abstract class BaseService<TEntity>
        where TEntity : class
    {
        private readonly IValidate<TEntity> _validate;
        protected readonly INotificationHandler _notification;

        public BaseService(INotificationHandler notification, IValidate<TEntity> validate)
        {
            _notification = notification;
            _validate = validate;
        }

        protected async Task<bool> ValidateAsync(TEntity entity)
        {
            var validationResult = await _validate.ValidateAsync(entity);

            if (validationResult.IsValid)
                return true;

            foreach (var error in validationResult.Errors)
                _notification.AddDomainNotification(error.PropertyName, error.ErrorMessage);

            return false;
        }
    }
}
