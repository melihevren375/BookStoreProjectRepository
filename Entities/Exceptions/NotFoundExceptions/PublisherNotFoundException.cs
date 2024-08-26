namespace Entities.Exceptions.NotFoundExceptions
{
    public class PublisherNotFoundException:NotFoundException
    {
        public PublisherNotFoundException(int id):base($"Publisher with ID number {id} was not found ")
        {
            
        }
    }
}
