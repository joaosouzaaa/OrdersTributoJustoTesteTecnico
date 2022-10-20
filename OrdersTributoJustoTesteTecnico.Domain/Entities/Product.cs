using OrdersTributoJustoTesteTecnico.Domain.Entities.EntitiesBase;

namespace OrdersTributoJustoTesteTecnico.Domain.Entities
{
    public sealed class Product : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public byte[]? Image { get; set; }

        public List<Order> Order { get; set; }
    }
}
