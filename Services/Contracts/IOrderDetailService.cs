using Entities.DataTransferObjects.OrderDetailsDataTransferObjects;
using Entities.RequestFeatures;
using System.Dynamic;

namespace Services.Contracts
{
    public interface IOrderDetailService
    {
        Task<(IEnumerable<ExpandoObject> orderDetailDtosForRead, MetaData metaData)> GetAllOrderDetailsAsync(OrderDetailParams orderDetailParams);
        Task<(IEnumerable<ExpandoObject> orderDetailDtosForDetails, MetaData metaData)> GetAllOrderDetailsWithDetailsAsync(OrderDetailParams orderDetailParams);
        Task DeleteOrderDetailAsync(Guid id, bool trackChanges);
        Task UpdateOrderDetailAsync(Guid id, bool trackChanges, OrderDetailForUpdate orderDetailDtoForUpdate);
        Task CreateOrderDetailAsync(OrderDetailForInsertion orderDetailDtoForInsertion);
    }
}
