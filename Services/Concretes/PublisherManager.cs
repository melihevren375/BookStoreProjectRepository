using AutoMapper;
using Entities.Concretes;
using Entities.DataTransferObjects.PublisherDataTransferObjects;
using Entities.Exceptions.NotFoundExceptions;
using Entities.RequestFeatures;
using Repositories.Contracts;
using Services.Contracts;
using System.Dynamic;

namespace Services.Concretes;

public class PublisherManager : IPublisherService
{
    private readonly IPublisherRepository _publisherRepository;
    private readonly IMapper _mapper;
    private readonly IDataShaper<PublisherDtoForRead> _dataShaper;

    public PublisherManager(IPublisherRepository publisherRepository, IMapper mapper, IDataShaper<PublisherDtoForRead> dataShaper)
    {
        _publisherRepository = publisherRepository;
        _mapper = mapper;
        _dataShaper = dataShaper;
    }

    public async Task CreatePublisherAsync(PublisherDtoForInsertion publisherDtoForInsertion)
    {
        var publisher = _mapper.Map<Publisher>(publisherDtoForInsertion);
        _publisherRepository.CreatePublisher(publisher);
    }

    public async Task DeletePublisherAsync(Guid id, bool trackChanges)
    {
        var publisher = await CheckhAndReturnPublisher(id, trackChanges);
        _publisherRepository.DeletePublisher(publisher);
    }

    public async Task<(IEnumerable<ExpandoObject> publiherDtosRead, MetaData metaData)> GetAllPublishersAsync(PublisherParams publisherParams)
    {
        var pagedListResults = await _publisherRepository.GetAllPublisherAsync(publisherParams);
        var publisherDtosForRead = _mapper.Map<IEnumerable<PublisherDtoForRead>>(pagedListResults);
        var shape = _dataShaper.ShapeData(publisherDtosForRead, publisherParams.Fields);

        return (publiherDtosRead: shape, metaData: pagedListResults.MetaData);
    }

    public async Task UpdatePublisherAsync(Guid id, bool trackChanges, PublisherDtoForUpdate publisherDtoForUpdate)
    {
        var publisher = await CheckhAndReturnPublisher(id, trackChanges);
        _mapper.Map(publisherDtoForUpdate, publisher);
        _publisherRepository.UpdatePublisher(publisher);
    }

    private async Task<Publisher> CheckhAndReturnPublisher(Guid id, bool trackChanges)
    {
        var publisher = await _publisherRepository.
            GetPublisherByConditionAsync(trackChanges, p => p.Id.Equals(id));

        if (publisher is null)
            throw new PublisherNotFoundException(id);

        return publisher;
    }
}
