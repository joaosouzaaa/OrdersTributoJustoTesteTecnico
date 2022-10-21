using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Requests.Client;
using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Responses.Client;
using OrdersTributoJustoTesteTecnico.Domain.Entities;
using TestsBuilders.BaseBuilders;

namespace TestsBuilders
{
    public class ClientBuilder : BaseBuilder
    {
        public static ClientBuilder NewObject() => new ClientBuilder();

        private int _age = GenerateRandomNumber();
        private string _cpf = "07587894444";
        private string _firstName = GenerateRandomWord();
        private string _lastName = GenerateRandomWord();
        private List<Order> _orders = new List<Order>();

        public Client DomainBuild() =>
            new Client()
            {
                Age = _age,
                Cpf = _cpf,
                FirstName = _firstName,
                Id = _id,
                LastName = _lastName,
                Orders = _orders
            };

        public ClientResponse ResponseBuild() =>
            new ClientResponse()
            {
                Age = _age,
                Cpf = _cpf,
                FirstName = _firstName,
                Id = _id,
                LastName = _lastName
            };

        public ClientSaveRequest SaveRequestBuild() =>
            new ClientSaveRequest()
            {
                Age = _age,
                Cpf = _cpf,
                FirstName = _firstName,
                LastName = _lastName
            };

        public ClientUpdateRequest UpdateRequestBuild() =>
            new ClientUpdateRequest()
            {
                Cpf = _cpf,
                Age = _age,
                FirstName = _firstName,
                Id = _id,
                LastName = _lastName
            };

        public ClientBuilder WithFirstName(string firstName)
        {
            _firstName = firstName;
            
            return this;
        }

        public ClientBuilder WithLastName(string lastName)
        {
            _lastName = lastName;

            return this;
        }

        public ClientBuilder WithCpf(string cpf)
        {
            _cpf = cpf;
            
            return this;
        }

        public ClientBuilder WithAge(int age)
        {
            _age = age;
            
            return this;
        }
    }
}
