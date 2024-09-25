namespace Entities.Exceptions.NotFoundExceptions
{
    public class CategoryNotFoundException : NotFoundException
    {
        public CategoryNotFoundException(Guid id) : base($"{id} numaralı category bulunamadı.")
        {
        }
    }
}
