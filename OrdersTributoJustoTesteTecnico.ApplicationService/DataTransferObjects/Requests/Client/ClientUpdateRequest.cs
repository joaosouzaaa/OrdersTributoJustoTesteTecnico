namespace OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Requests.Client
{
    public sealed class ClientUpdateRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Cpf { get; set; }
        public int Age { get; set; }
    }
}
