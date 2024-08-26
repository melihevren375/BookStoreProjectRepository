namespace Entities.Exceptions.NotFoundExceptions
{
    public class CategoryNotFoundException : NotFoundException
    {
        public CategoryNotFoundException(int id) : base($"{id} numaralı category bulunamadı.")
        {
        }
    }
}
