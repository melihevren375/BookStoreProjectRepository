namespace Entities.Exceptions.NotFoundExceptions
{
    public class OrderDetailNotFoundException:NotFoundException
    {
        public OrderDetailNotFoundException(Guid id):base($"OrderDetail with ID number {id} was not found ")
        {
            
        }
    }
}
