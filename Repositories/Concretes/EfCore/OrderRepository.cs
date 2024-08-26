using Entities.Concretes;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Concretes.EfCore.Repository_Extensions;
using Repositories.Contracts;
using System.Linq.Expressions;

namespace Repositories.Concretes.EfCore
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateOrder(Order order) => CreateEntity(order);

        public async Task<int> DailyTotalOrderCountAsync()
        {
            var count = await GetAllEntities()
           .Where(o => o.OrderDate.Date == DateTime.Today.Date)
           .CountAsync();


            return count;
        }

        public void DeleteOrder(Order order) => DeleteEntity(order);

        public async Task<PagedList<Order>> GetAllOrderAsync(OrderParams orderParams)
        {
            var orders = await GetAllEntities().
                FilterMinAndMaxDate(orderParams.MinOrderTime, orderParams.MaxOrderTime).
                FilterMinAndMaxTotalAmount(orderParams.MinTotalAmount, orderParams.MaxTotalAmount).
                ToListAsync();

            return PagedList<Order>.ToPagedList(orders, orderParams.PageSize, orderParams.PageNumber);
        }

        public async Task<Order> GetOrderByConditionAsync(bool trackChanges, Expression<Func<Order, bool>> expression)
        {
            var order = await GetEntitiesByCondition(trackChanges, expression).
                SingleOrDefaultAsync();

            return order;
        }

        public async Task<int> MounhtlyTotalOrderCountAsync()
        {
            var startOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

            var count = await GetAllEntities()
                .Where(o => o.OrderDate.Date >= startOfMonth.Date && o.OrderDate.Date <= endOfMonth.Date)
                .CountAsync();

            return count;
        }

        public void UpdateOrder(Order order) => UpdateEntity(order);

        public async Task<int> WeeklyTotalOrderCountAsync()
        {
            var startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
            var endOfWeek = startOfWeek.AddDays(6);

            var count = await GetAllEntities()
                .Where(o => o.OrderDate.Date >= startOfWeek.Date && o.OrderDate.Date <= endOfWeek.Date)
                .CountAsync();

            return count;
        }
    }
}
