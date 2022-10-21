using FluentValidation;
using OrdersTributoJustoTesteTecnico.Business.Extensions;
using OrdersTributoJustoTesteTecnico.Domain.Entities;
using OrdersTributoJustoTesteTecnico.Domain.Enum;

namespace OrdersTributoJustoTesteTecnico.Business.Settings.ValidationSettings.EntitiesValidation
{
    public sealed class ClientValidation : Validate<Client>
    {
        public ClientValidation()
        {
            RuleFor(c => c.FirstName).Length(3, 50)
               .WithMessage(c => string.IsNullOrWhiteSpace(c.FirstName)
              ? EMessage.Required.Description().FormatTo("First Name")
              : EMessage.MoreExpected.Description().FormatTo("First Name", "3 to 50"));

            RuleFor(c => c.LastName).Length(3, 50)
               .WithMessage(c => string.IsNullOrWhiteSpace(c.LastName)
              ? EMessage.Required.Description().FormatTo("Last Name")
              : EMessage.MoreExpected.Description().FormatTo("Last Name", "3 to 50"));

            RuleFor(c => c.Cpf.CleanCaracters()).Length(11)
               .WithMessage(EMessage.MoreExpected.Description().FormatTo("Cpf", "11"));

            RuleFor(c => c.Age).GreaterThan(0)
                .WithMessage(EMessage.GreaterThan0.Description().FormatTo("Age"));
        }
    }
}
