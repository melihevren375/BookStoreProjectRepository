namespace Entities.Exceptions.NotFoundExceptions
{
    public class OrderNotFoundException:NotFoundException
    {
        public OrderNotFoundException(Guid id):base($"Order with ID number {id} was not found ")
        {
            
        }
    }
}
