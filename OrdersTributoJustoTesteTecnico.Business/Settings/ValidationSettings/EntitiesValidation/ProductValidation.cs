using FluentValidation;
using OrdersTributoJustoTesteTecnico.Business.Extensions;
using OrdersTributoJustoTesteTecnico.Domain.Entities;
using OrdersTributoJustoTesteTecnico.Domain.Enum;

namespace OrdersTributoJustoTesteTecnico.Business.Settings.ValidationSettings.EntitiesValidation
{
    public sealed class ProductValidation : Validate<Product>
    {
        public ProductValidation()
        {
            RuleFor(p => p.Name).Length(3, 100)
               .WithMessage(p => string.IsNullOrWhiteSpace(p.Name)
              ? EMessage.Required.Description().FormatTo("Name")
              : EMessage.MoreExpected.Description().FormatTo("Name", "3 to 100"));

            RuleFor(p => p.Price).GreaterThan(0)
                .WithMessage(EMessage.GreaterThan0.Description().FormatTo("Price"));
        }
    }
}
