using Entities.Concretes;
using Entities.RequestFeatures;
using System.Linq.Expressions;

namespace Repositories.Contracts
{
    public interface IOrderDetailRepository : IBaseRepository<OrderDetail>
    {
        Task<PagedList<OrderDetail>> GetAllOrderDetailAsync(OrderDetailParams orderDetailParams);
        Task<PagedList<OrderDetail>> GetAllOrderDetailsWithDetailsAsync(OrderDetailParams orderDetailParams);
        Task<OrderDetail> GetOrderDetailByConditionAsync(bool trackChanges, Expression<Func<OrderDetail, bool>> expression);
        void CreateOrderDetail(OrderDetail orderDetail);
        void UpdateOrderDetail(OrderDetail orderDetail);
        void DeleteOrderDetail(OrderDetail orderDetail);
    }
}
