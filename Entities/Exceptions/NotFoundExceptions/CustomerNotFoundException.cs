namespace Entities.Exceptions.NotFoundExceptions
{
    public class CustomerNotFoundException:NotFoundException
    {
        public CustomerNotFoundException(int id):base($"Customer with ID number {id} was not found ")
        {
            
        }
    }
}
