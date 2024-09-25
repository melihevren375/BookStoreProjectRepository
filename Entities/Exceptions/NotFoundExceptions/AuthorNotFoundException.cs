namespace Entities.Exceptions.NotFoundExceptions
{
    public class AuthorNotFoundException:NotFoundException
    {
        public AuthorNotFoundException(Guid id):base($"Author with ID number {id} was not found ")
        {
            
        }
    }
}
