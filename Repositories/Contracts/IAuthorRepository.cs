using Entities.Concretes;
using Entities.RequestFeatures;
using System.Linq.Expressions;

namespace Repositories.Contracts
{
    public interface IAuthorRepository : IBaseRepository<Author>
    {
        Task<PagedList<Author>> GetAllAuthorsAsync(AuthorParams authorParams);
        Task<Author> GetAuthorByConditionAsync(bool trackChanges, Expression<Func<Author, bool>> expression);
        void CreateAuthor(Author author);
        void UpdateAuthor(Author author);
        void DeleteAuthor(Author author);
    }
}
