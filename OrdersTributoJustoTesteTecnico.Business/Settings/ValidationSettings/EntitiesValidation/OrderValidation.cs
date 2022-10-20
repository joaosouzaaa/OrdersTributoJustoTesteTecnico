using FluentValidation;
using OrdersTributoJustoTesteTecnico.Business.Extensions;
using OrdersTributoJustoTesteTecnico.Domain.Entities;
using OrdersTributoJustoTesteTecnico.Domain.Enum;

namespace OrdersTributoJustoTesteTecnico.Business.Settings.ValidationSettings.EntitiesValidation
{
    public sealed class OrderValidation : Validate<Order>
    {
        public OrderValidation()
        {
            RuleFor(o => o.Quantity).GreaterThan(0)
                .WithMessage(EMessage.GreaterThan0.Description().FormatTo("Quantity"));

            RuleFor(o => o.TotalPrice).GreaterThan(0)
                .WithMessage(EMessage.GreaterThan0.Description().FormatTo("TotalPrice"));
        }
    }
}
