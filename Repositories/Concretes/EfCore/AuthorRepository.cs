using Entities.Concretes;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Concretes.EfCore.Repository_Extensions;
using Repositories.Contracts;
using System.Linq.Expressions;

namespace Repositories.Concretes.EfCore
{
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateAuthor(Author author) => CreateEntity(author);

        public void DeleteAuthor(Author author) => DeleteEntity(author);

        public async Task<PagedList<Author>> GetAllAuthorsAsync(AuthorParams authorParams)
        {
            var authors = await GetAllEntities().
                FirstNameAndLastNameSearch(authorParams.NameSearchTerm).
                ToListAsync();

            return PagedList<Author>.
                ToPagedList(authors, authorParams.PageSize, authorParams.PageNumber);
        }

        public async Task<Author> GetAuthorByConditionAsync(bool trackChanges, Expression<Func<Author, bool>> expression)
        {
            var author = await GetEntitiesByCondition(trackChanges, expression).
                SingleOrDefaultAsync();

            return author;
        }

        public void UpdateAuthor(Author author) => UpdateEntity(author);

    }
}
