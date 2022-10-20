using OrdersTributoJustoTesteTecnico.Domain.Entities.EntitiesBase;

namespace OrdersTributoJustoTesteTecnico.Domain.Entities
{
    public sealed class Order : BaseEntity
    {
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }
        public List<Product> Products { get; set; }
    }
}
