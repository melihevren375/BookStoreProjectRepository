namespace Entities.Exceptions.NotFoundExceptions
{
    public class OrderNotFoundException:NotFoundException
    {
        public OrderNotFoundException(int id):base($"Order with ID number {id} was not found ")
        {
            
        }
    }
}
