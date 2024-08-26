using Entities.Concretes;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Concretes.EfCore.Extensions;
using Repositories.Contracts;
using System.Linq.Expressions;

namespace Repositories.Concretes.EfCore
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateBook(Book book) => CreateEntity(book);

        public void DeleteBook(Book book) => DeleteEntity(book);

        public async Task<PagedList<Book>> GetAllBookAsync(BookParams bookParams)
        {
            var books = await GetAllEntities().
                FilterPrice(bookParams.MinPrice, bookParams.MaxPrice).
                Search(bookParams.SearchTerm).
                ToListAsync();

            return PagedList<Book>.ToPagedList(books, bookParams.PageSize, bookParams.PageNumber);
        }

        public async Task<Book> GetBookByConditionAsync(bool trackChanges, Expression<Func<Book, bool>> expression)
        {
            var book = await GetEntitiesByCondition(trackChanges, expression).
                SingleOrDefaultAsync();

            return book;
        }

        public void UpdateBook(Book book) => UpdateEntity(book);

        public async Task<PagedList<Book>> GetBooksWithDetailsAsync(BookParams bookParams)
        {
            var results = await GetAllEntities()
                .FilterPrice(bookParams.MinPrice, bookParams.MaxPrice)
                .Search(bookParams.SearchTerm)
                .Include(b => b.Category)
                .Include(b => b.Publisher)
                .Include(b => b.Author)
                .ToListAsync();

            return PagedList<Book>.ToPagedList(results, bookParams.PageSize, bookParams.PageNumber);
        }

        public async Task<List<string>> GetTheMostPopularBooksAsync()
        {
            var results = await (from od in _repositoryContext.OrderDetails
                                 join b in _repositoryContext.Books on od.BookId equals b.Id
                                 group od by b.Title into g
                                 orderby g.Sum(x => x.Quantity) descending
                                 select g.Key)
                                 .Take(5)
                                 .ToListAsync();

            return results;
        }

    }
}
