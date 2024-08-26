using Entities.Concretes;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Concretes.EfCore.Repository_Extensions;
using Repositories.Contracts;
using System.Linq.Expressions;

namespace Repositories.Concretes.EfCore
{
    public class OrderDetailRepository : BaseRepository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateOrderDetail(OrderDetail orderDetail) => CreateEntity(orderDetail);

        public void DeleteOrderDetail(OrderDetail orderDetail) => DeleteEntity(orderDetail);

        public async Task<PagedList<OrderDetail>> GetAllOrderDetailAsync(OrderDetailParams orderDetailParams)
        {
            var orderDetails = await GetAllEntities().
                FilterMinAndMaxPrice(orderDetailParams.MinPrice, orderDetailParams.MaxPrice).
                ToListAsync();

            return PagedList<OrderDetail>.ToPagedList(orderDetails, orderDetailParams.PageSize, orderDetailParams.PageNumber);
        }

        public async Task<OrderDetail> GetOrderDetailByConditionAsync(bool trackChanges, Expression<Func<OrderDetail, bool>> expression)
        {
            var orderDetail = await GetEntitiesByCondition(trackChanges, expression).
                SingleOrDefaultAsync();

            return orderDetail;
        }

        public void UpdateOrderDetail(OrderDetail orderDetail) => UpdateEntity(orderDetail);

        public async Task<PagedList<OrderDetail>> GetAllOrderDetailsWithDetailsAsync(OrderDetailParams orderDetailParams)
        {
            var results = await GetAllEntities().
               FilterMinAndMaxPrice(orderDetailParams.MinPrice, orderDetailParams.MaxPrice).
               Include(od => od.Book).
               ToListAsync();

            return PagedList<OrderDetail>.ToPagedList(results, orderDetailParams.PageSize, orderDetailParams.PageNumber);
        }
    }
}
