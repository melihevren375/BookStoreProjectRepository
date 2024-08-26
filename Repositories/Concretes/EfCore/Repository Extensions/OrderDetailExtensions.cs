using Entities.Concretes;

namespace Repositories.Concretes.EfCore.Repository_Extensions
{
    public static class OrderDetailExtensions
    {
        public static IQueryable<OrderDetail> FilterMinAndMaxPrice(this IQueryable<OrderDetail> orderDetails, decimal minPrice, decimal maxPrice)
        {
            if (maxPrice == 0)
                maxPrice = decimal.MaxValue;

            var results = orderDetails.
                Where(od => od.Price < maxPrice
                &&
                od.Price > minPrice
                );

            return results;
        }
    }
}
