using OrdersTributoJustoTesteTecnico.ApplicationService.AutoMapperSettings;
using OrdersTributoJustoTesteTecnico.ApplicationService.DataTransferObjects.Responses.Order;
using OrdersTributoJustoTesteTecnico.Business.Settings.PaginationSettings;
using OrdersTributoJustoTesteTecnico.Domain.Entities;
using TestsBuilders;
using TestsBuilders.Helpers;

namespace UnitTests.AutoMapperTests
{
    public sealed class OrderAutoMapperTests
    {
        public OrderAutoMapperTests()
        {
            AutoMapperConfigurations.Inicialize();
        }

        [Fact]
        public void Order_To_OrderResponse()
        {
            var order = OrderBuilder.NewObject().DomainBuild();
            var orderResponse = order.MapTo<Order, OrderResponse>();

            Assert.Equal(orderResponse.Id, order.Id);
            Assert.Equal(orderResponse.Quantity, order.Quantity);
            Assert.Equal(orderResponse.TotalPrice, order.TotalPrice);
        }

        [Fact]
        public void OrderPageList_To_OrderResponsePageList()
        {
            var orderList = new List<Order>()
            {
                OrderBuilder.NewObject().DomainBuild(),
                OrderBuilder.NewObject().DomainBuild(),
                OrderBuilder.NewObject().DomainBuild()
            };
            var orderPageList = BuildersUtils.BuildPageList(orderList);
            var orderResponsePageList = orderPageList.MapTo<PageList<Order>, PageList<OrderResponse>>();

            Assert.Equal(orderResponsePageList.Result.Count, orderPageList.Result.Count);
            Assert.Equal(orderResponsePageList.TotalCount, orderPageList.TotalCount);
            Assert.Equal(orderResponsePageList.PageSize, orderPageList.PageSize);
            Assert.Equal(orderResponsePageList.CurrentPage, orderPageList.CurrentPage);
            Assert.Equal(orderResponsePageList.TotalPages, orderPageList.TotalPages);
        }
    }
}
