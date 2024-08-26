using Entities.Concretes;
using Entities.DataTransferObjects.OrderDataTransferObjects;
using Entities.RequestFeatures;
using System.Dynamic;

namespace Services.Contracts
{
    public interface IOrderService
    {
        Task CreateOrderAsync(OrderDtoForInsertion orderDtoForInsertion);
        Task UpdateOrderAsync(int id, bool trackChanges, OrderDtoForUpdate orderDtoForUpdate);
        Task DeleteOrderAsync(int id, bool trackChanges);
        Task<int> DailyTotalOrderCountAsync();
        Task<int> WeeklyTotalOrderCountAsync();
        Task<int> MounthlyTotalOrderCountAsync();
        Task<(IEnumerable<ExpandoObject> orderDtosForRead, MetaData metaData)> GetAllOrdersAsync(OrderParams orderParams);
        public (decimal totalAmount, List<OrderDetail> orderDetails) CalculateTotalAmount(string values, string quantities, Guid orderId);
    }
}
