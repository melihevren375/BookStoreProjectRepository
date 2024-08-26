using Entities.Concretes;

namespace Repositories.Concretes.EfCore.Repository_Extensions
{
    public static class OrdersExtensions
    {
        public static IQueryable<Order> FilterMinAndMaxDate(this IQueryable<Order> orders, DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
                minDate = DateTime.MinValue;

            if (!maxDate.HasValue)
                maxDate = DateTime.MaxValue;

            var results = orders
                .Where(o => o.OrderDate < maxDate && o.OrderDate > minDate);

            return results;
        }

        public static IQueryable<Order> FilterMinAndMaxTotalAmount(this IQueryable<Order> orders, decimal minTotalAmount, decimal maxTotalAmount)
        {
            if (maxTotalAmount == 0)
                maxTotalAmount = decimal.MaxValue;

            var results = orders
                .Where(o => o.TotalAmount < maxTotalAmount && o.TotalAmount > minTotalAmount);

            return results;
        }
    }

}
