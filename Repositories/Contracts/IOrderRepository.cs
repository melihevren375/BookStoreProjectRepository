using Entities.Concretes;
using Entities.RequestFeatures;
using System.Linq.Expressions;

namespace Repositories.Contracts
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<PagedList<Order>> GetAllOrderAsync(OrderParams orderParams);
        Task<Order> GetOrderByConditionAsync(bool trackChanges, Expression<Func<Order, bool>> expression);
        Task<int> DailyTotalOrderCountAsync(); 
        Task<int> WeeklyTotalOrderCountAsync(); 
        Task<int> MounhtlyTotalOrderCountAsync(); 
        void CreateOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(Order order);
    }
}
