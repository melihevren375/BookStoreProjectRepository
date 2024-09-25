using Entities.DataTransferObjects.PublisherDataTransferObjects;
using Entities.RequestFeatures;
using System.Dynamic;

namespace Services.Contracts;

public interface IPublisherService
{
    Task CreatePublisherAsync(PublisherDtoForInsertion publisherDtoForInsertion);
    Task UpdatePublisherAsync(Guid id, bool trackChanges, PublisherDtoForUpdate publisherDtoForUpdate);
    Task DeletePublisherAsync(Guid id, bool trackChanges);
    Task<(IEnumerable<ExpandoObject> publiherDtosRead, MetaData metaData)> GetAllPublishersAsync(PublisherParams publisherParams);
}
