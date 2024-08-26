using Entities.Concretes;
using Entities.RequestFeatures;
using System.Linq.Expressions;

namespace Repositories.Contracts
{
    public interface IPublisherRepository : IBaseRepository<Publisher>
    {
        Task<PagedList<Publisher>> GetAllPublisherAsync(PublisherParams publisherParams);
        Task<Publisher> GetPublisherByConditionAsync(bool trackChanges, Expression<Func<Publisher, bool>> expression);
        void CreatePublisher(Publisher publisher);
        void UpdatePublisher(Publisher publisher);
        void DeletePublisher(Publisher publisher);
    }
}
