using Entities.Concretes;
using Entities.RequestFeatures;
using System.Linq.Expressions;

namespace Repositories.Contracts
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        Task<PagedList<Book>> GetAllBookAsync(BookParams bookParams);
        Task<Book> GetBookByConditionAsync(bool trackChanges, Expression<Func<Book, bool>> expression);
        void CreateBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(Book book);
        Task<PagedList<Book>> GetBooksWithDetailsAsync(BookParams bookParams);
        Task<List<string>> GetTheMostPopularBooksAsync();
    }
}
