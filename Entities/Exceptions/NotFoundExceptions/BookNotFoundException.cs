namespace Entities.Exceptions.NotFoundExceptions
{
    public sealed class BookNotFoundException : NotFoundException
    {
        public BookNotFoundException(Guid id) : base($"{id} numaralı kitap bulunamadı!")
        {
        }
    }
}
