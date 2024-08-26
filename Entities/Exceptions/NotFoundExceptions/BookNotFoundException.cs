namespace Entities.Exceptions.NotFoundExceptions
{
    public sealed class BookNotFoundException : NotFoundException
    {
        public BookNotFoundException(int id) : base($"{id} numaralı kitap bulunamadı!")
        {
        }
    }
}
