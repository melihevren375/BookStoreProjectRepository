using Entities.Contracts;

namespace Entities.Concretes
{
    public class Customer : IEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public List<Order>? Orders { get; set; }
        public EmailCode EmailCode { get; set; }
    }
}
