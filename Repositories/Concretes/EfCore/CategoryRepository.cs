using Entities.Concretes;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Concretes.EfCore.Repository_Extensions;
using Repositories.Contracts;
using System.Linq.Expressions;

namespace Repositories.Concretes.EfCore
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateCategory(Category category) => CreateEntity(category);

        public void DeleteCategory(Category category) => DeleteEntity(category);

        public async Task<PagedList<Category>> GetAllCategoriesAsync(CategoryParams categoryParams)
        {
            var categories = await GetAllEntities().
                Search(categoryParams.NameSearchTerm).
                ToListAsync();

            return PagedList<Category>.
                ToPagedList(categories, categoryParams.PageSize, categoryParams.PageNumber);
        }

        public async Task<Category> GetOneCategoryByConditionAsync(Expression<Func<Category, bool>> expression, bool trackChanges)
        {
            var category = await GetEntitiesByCondition(trackChanges, expression).
                SingleOrDefaultAsync();

            return category;
        }

        public void UpdateCategory(Category category) => UpdateEntity(category);
    }
}
