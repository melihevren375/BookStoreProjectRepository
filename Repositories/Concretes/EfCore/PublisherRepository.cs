using Entities.Concretes;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Concretes.EfCore.Repository_Extensions;
using Repositories.Contracts;
using System.Linq.Expressions;

namespace Repositories.Concretes.EfCore
{
    public class PublisherRepository : BaseRepository<Publisher>, IPublisherRepository
    {
        public PublisherRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreatePublisher(Publisher publisher) => CreateEntity(publisher);

        public void DeletePublisher(Publisher publisher) => DeleteEntity(publisher);

        public async Task<PagedList<Publisher>> GetAllPublisherAsync(PublisherParams publisherParams)
        {
            var publishers = await GetAllEntities().
                SearchName(publisherParams.NameSearchTerm).
                ToListAsync();

            return PagedList<Publisher>.ToPagedList(publishers, publisherParams.PageSize, publisherParams.PageNumber);
        }

        public async Task<Publisher> GetPublisherByConditionAsync(bool trackChanges, Expression<Func<Publisher, bool>> expression)
        {
            var publisher = await GetEntitiesByCondition(trackChanges, expression).
                SingleOrDefaultAsync();

            return publisher;
        }

        public void UpdatePublisher(Publisher publisher) => UpdateEntity(publisher);
    }
}
