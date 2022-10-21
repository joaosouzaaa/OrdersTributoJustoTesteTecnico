using OrdersTributoJustoTesteTecnico.Business.Settings.ValidationSettings.EntitiesValidation;
using TestsBuilders;

namespace UnitTests.ValidationTests
{
    public sealed class ProductValidationTests
    {
        ProductValidation _validation;

        public ProductValidationTests()
        {
            _validation = new ProductValidation();
        }

        [Fact]
        public async Task ValidateAsync_DomainProduct_Assert_IsValid_Equals_True()
        {
            var product = ProductBuilder.NewObject().DomainBuild();

            var validationResult = await _validation.ValidateAsync(product);

            Assert.True(validationResult.IsValid);
        }

        [Theory]
        [InlineData("a")]
        [InlineData("")]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public async Task ValidateAsync_Product_Name_Invalid_Assert_Is_Valid_Equals_False(string name)
        {
            var product = ProductBuilder.NewObject().WithName(name).DomainBuild();

            var validationResult = await _validation.ValidateAsync(product);

            Assert.False(validationResult.IsValid);
        }

        [Theory]
        [InlineData(-90)]
        public async Task ValidateAsync_Product_Price_Invalid_Assert_Is_Valid_Equals_False(decimal price)
        {
            var product = ProductBuilder.NewObject().WithPrice(price).DomainBuild();

            var validationResult = await _validation.ValidateAsync(product);

            Assert.False(validationResult.IsValid);
        }
    }
}
