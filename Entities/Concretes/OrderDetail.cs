using Entities.Contracts;

namespace Entities.Concretes
{
    public class OrderDetail : IEntity
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid BookId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Order Order { get; set; }
        public Book Book { get; set; }
    }
}
