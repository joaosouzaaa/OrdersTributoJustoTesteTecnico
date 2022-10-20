namespace OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Requests.Client
{
    public sealed class ClientSaveRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Cpf { get; set; }
        public int Age { get; set; }
    }
}
