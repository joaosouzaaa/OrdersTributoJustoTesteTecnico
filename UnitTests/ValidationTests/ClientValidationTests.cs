using OrdersTributoJustoTesteTecnico.Business.Settings.ValidationSettings.EntitiesValidation;
using TestsBuilders;

namespace UnitTests.ValidationTests
{
    public sealed class ClientValidationTests
    {
        ClientValidation _validation;

        public ClientValidationTests()
        {
            _validation = new ClientValidation();
        }

        [Fact]
        public async Task ValidateAsync_DomainClient_Assert_IsValid_Equals_True()
        {
            var client = ClientBuilder.NewObject().DomainBuild();

            var validationResult = await _validation.ValidateAsync(client);

            Assert.True(validationResult.IsValid);
        }

        [Theory]
        [InlineData("a")]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        [InlineData("aa")]
        [InlineData("")]
        public async Task ValidateAsync_Client_FirstName_Invalid_Assert_Is_Valid_Equals_False(string firstName)
        {
            var client = ClientBuilder.NewObject().WithFirstName(firstName).DomainBuild();

            var validationResult = await _validation.ValidateAsync(client);

            Assert.False(validationResult.IsValid);
        }

        [Theory]
        [InlineData("a")]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        [InlineData("")]
        public async Task ValidateAsync_Client_LastName_Invalid_Assert_Is_Valid_Equals_False(string lastName)
        {
            var client = ClientBuilder.NewObject().WithLastName(lastName).DomainBuild();

            var validationResult = await _validation.ValidateAsync(client);

            Assert.False(validationResult.IsValid);
        }

        [Theory]
        [InlineData("alalal")]
        [InlineData("lorem")]
        [InlineData("aaaaaaaaaaaaa")]
        [InlineData("a")]
        public async Task ValidateAsync_Client_Cpf_Invalid_Assert_Is_Valid_Equals_False(string cpf)
        {
            var client = ClientBuilder.NewObject().WithCpf(cpf).DomainBuild();

            var validationResult = await _validation.ValidateAsync(client);

            Assert.False(validationResult.IsValid);
        }

        [Theory]
        [InlineData(-20)]
        [InlineData(-2)]
        public async Task ValidateAsync_Client_Age_Invalid_Assert_Is_Valid_Equals_False(int age)
        {
            var client = ClientBuilder.NewObject().WithAge(age).DomainBuild();

            var validationResult = await _validation.ValidateAsync(client);

            Assert.False(validationResult.IsValid);
        }
    }
}
