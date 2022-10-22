using Microsoft.AspNetCore.Mvc;
using OrdersTributoJustoTesteTecnico.Business.Settings.NotificationSettings;

namespace OrdersTributoJustoTesteTecnico.Api.ControllersAttributes
{
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IEnumerable<DomainNotification>))]
    public class CommandsResponseTypes : Attribute
    {
    }
}
