using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OrdersTributoJustoTesteTecnico.Business.Interfaces.Notification;

namespace OrdersTributoJustoTesteTecnico.Api.Filters
{
    public sealed class NotificationFilter : ActionFilterAttribute
    {
        private readonly INotificationHandler _notification;

        public NotificationFilter(INotificationHandler notification)
        {
            _notification = notification;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var domainNotificationList = _notification.GetDomainNotificationList();

            if (domainNotificationList.Any())
                context.Result = new BadRequestObjectResult(domainNotificationList);

            base.OnActionExecuted(context);
        }
    }
}
