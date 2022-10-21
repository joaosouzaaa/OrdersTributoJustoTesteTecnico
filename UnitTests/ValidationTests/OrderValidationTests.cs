using OrdersTributoJustoTesteTecnico.Business.Settings.ValidationSettings.EntitiesValidation;
using OrdersTributoJustoTesteTecnico.Domain.Entities;
using TestsBuilders;

namespace UnitTests.ValidationTests
{
    public sealed class OrderValidationTests
    {
        OrderValidation _validation;

        public OrderValidationTests()
        {
            _validation = new OrderValidation();
        }

        [Fact]
        public async Task ValidateAsync_DomainOrder_Assert_IsValid_Equals_True()
        {
            var order = OrderBuilder.NewObject().DomainBuild();

            var validationResult = await _validation.ValidateAsync(order);

            Assert.True(validationResult.IsValid);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-20)]
        public async Task ValidateAsync_Order_Quantity_Invalid_Assert_Is_Valid_Equals_False(int quantity)
        {
            var order = OrderBuilder.NewObject().WithQuantity(quantity).DomainBuild();

            var validationResult = await _validation.ValidateAsync(order);

            Assert.False(validationResult.IsValid);
        }

        [Theory]
        [InlineData(-150)]
        [InlineData(-20)]
        public async Task ValidateAsync_Order_TotalPrice_Invalid_Assert_Is_Valid_Equals_False(decimal totalPrice)
        {
            var order = OrderBuilder.NewObject().WithTotalPrice(totalPrice).DomainBuild();

            var validationResult = await _validation.ValidateAsync(order);

            Assert.False(validationResult.IsValid);
        }
    }
}
