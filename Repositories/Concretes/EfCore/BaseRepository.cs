using Entities.Contracts;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System.Linq.Expressions;

namespace Repositories.Concretes.EfCore
{
    public abstract class BaseRepository<T> : IBaseRepository<T>
        where T : class, IEntity, new()
    {
        protected readonly RepositoryContext _repositoryContext;

        protected BaseRepository(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public void CreateEntity(T entity)
        {
            _repositoryContext.
                Set<T>().
                Add(entity);

            _repositoryContext.SaveChanges();
        }

        public void DeleteEntity(T entity)
        {
            _repositoryContext.
                Set<T>().
                Remove(entity);

            _repositoryContext.SaveChanges();
        }

        public IQueryable<T> GetAllEntities()
        {
            var entities = _repositoryContext.
                 Set<T>().
                 AsNoTracking();

            return entities;
        }

        public IQueryable<T> GetEntitiesByCondition(bool trackChanges, Expression<Func<T, bool>> expression)
        {
            var entities = !trackChanges ?
                _repositoryContext.
                Set<T>().
                Where(expression).
                AsNoTracking() :

                _repositoryContext.
                Set<T>().
                Where(expression);

            return entities;
        }

        public void UpdateEntity(T entity)
        {
            _repositoryContext.
                Set<T>().
                Update(entity);

            _repositoryContext.SaveChanges();
        }
    }
}
