using Entities.Contracts;

namespace Entities.Concretes
{
    public class Author : IEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Book>? Books { get; set; }
    }
}
