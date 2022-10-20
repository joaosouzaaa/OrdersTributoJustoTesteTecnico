using OrdersTributoJustoTesteTecnico.Domain.Entities.EntitiesBase;

namespace OrdersTributoJustoTesteTecnico.Domain.Entities
{
    public sealed class Client : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Cpf { get; set; }
        public int Age { get; set; }

        public List<Order> Orders { get; set; }
    }
}
