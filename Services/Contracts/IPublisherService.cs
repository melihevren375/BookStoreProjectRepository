using Entities.DataTransferObjects.PublisherDataTransferObjects;
using Entities.RequestFeatures;
using System.Dynamic;

namespace Services.Contracts;

public interface IPublisherService
{
    Task CreatePublisherAsync(PublisherDtoForInsertion publisherDtoForInsertion);
    Task UpdatePublisherAsync(int id, bool trackChanges, PublisherDtoForUpdate publisherDtoForUpdate);
    Task DeletePublisherAsync(int id, bool trackChanges);
    Task<(IEnumerable<ExpandoObject> publiherDtosRead, MetaData metaData)> GetAllPublishersAsync(PublisherParams publisherParams);
}
