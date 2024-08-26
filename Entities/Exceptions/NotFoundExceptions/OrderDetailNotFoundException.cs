namespace Entities.Exceptions.NotFoundExceptions
{
    public class OrderDetailNotFoundException:NotFoundException
    {
        public OrderDetailNotFoundException(int id):base($"OrderDetail with ID number {id} was not found ")
        {
            
        }
    }
}
