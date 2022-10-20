using System.ComponentModel;

namespace OrdersTributoJustoTesteTecnico.Domain.Enum
{
    public enum EMessage
    {
        [Description("{0} need to be filled")]
        Required,

        [Description("Field {0} allows {1} chars")]
        MoreExpected,

        [Description("{0} not found")]
        NotFound,

        [Description("{0} has to be greater than 0")]
        GreaterThan0
    }
}
