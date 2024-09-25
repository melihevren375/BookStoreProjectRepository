namespace Entities.Exceptions.NotFoundExceptions
{
    public class PublisherNotFoundException:NotFoundException
    {
        public PublisherNotFoundException(Guid id):base($"Publisher with ID number {id} was not found ")
        {
            
        }
    }
}
