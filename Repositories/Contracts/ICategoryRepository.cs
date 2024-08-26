using Entities.Concretes;
using Entities.RequestFeatures;
using System.Linq.Expressions;

namespace Repositories.Contracts
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<PagedList<Category>> GetAllCategoriesAsync(CategoryParams categoryParams);
        Task<Category> GetOneCategoryByConditionAsync(Expression<Func<Category, bool>> expression, bool trackChanges);
        void CreateCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
    }
}
