using Entities.DataTransferObjects.OrderDetailsDataTransferObjects;
using Entities.RequestFeatures;
using System.Dynamic;

namespace Services.Contracts
{
    public interface IOrderDetailService
    {
        Task<(IEnumerable<ExpandoObject> orderDetailDtosForRead, MetaData metaData)> GetAllOrderDetailsAsync(OrderDetailParams orderDetailParams);
        Task<(IEnumerable<ExpandoObject> orderDetailDtosForDetails, MetaData metaData)> GetAllOrderDetailsWithDetailsAsync(OrderDetailParams orderDetailParams);
        Task DeleteOrderDetailAsync(int id, bool trackChanges);
        Task UpdateOrderDetailAsync(int id, bool trackChanges, OrderDetailForUpdate orderDetailDtoForUpdate);
        Task CreateOrderDetailAsync(OrderDetailForInsertion orderDetailDtoForInsertion);
    }
}
