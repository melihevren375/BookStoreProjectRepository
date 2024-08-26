using Entities.Contracts;

namespace Entities.Concretes
{
    public class EmailCode : IEntity
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public int? Code { get; set; }

        public Customer Customer { get; set; }
    }
}
