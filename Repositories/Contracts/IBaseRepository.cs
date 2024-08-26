using System.Linq.Expressions;

namespace Repositories.Contracts
{
    public interface IBaseRepository<T>
    {
        IQueryable<T> GetAllEntities();
        IQueryable<T> GetEntitiesByCondition(bool trackChanges, Expression<Func<T, bool>> expression);
        void UpdateEntity(T entity);
        void CreateEntity(T entity);
        void DeleteEntity(T entity);
    }
}
