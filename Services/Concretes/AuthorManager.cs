using AutoMapper;
using Entities.Concretes;
using Entities.DataTransferObjects.AutherDataTransferObjects;
using Entities.Exceptions.NotFoundExceptions;
using Entities.RequestFeatures;
using Repositories.Contracts;
using Services.Contracts;
using System.Dynamic;

namespace Services.Concretes;

public class AuthorManager : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;
    private readonly IDataShaper<AutherDtoForRead> _dataShaper;

    public AuthorManager(IAuthorRepository authorRepository, IMapper mapper, IDataShaper<AutherDtoForRead> dataShaper)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
        _dataShaper = dataShaper;
    }

    public async Task CreateAuthorAsync(AutherDtoForInsertion autherDtoForInsertion)
    {
        var author = _mapper.Map<Author>(autherDtoForInsertion);
        _authorRepository.CreateAuthor(author);
    }

    public async Task DeleteAuthorAsync(int id, bool trackChanges)
    {
        var author = await CheckAndReturnAuthor(id, trackChanges);

        _authorRepository.DeleteAuthor(author);
    }

    public async Task<(IEnumerable<ExpandoObject> authorDtosForRead, MetaData metaData)> GetAllAuthorsAsync(AuthorParams authorParams)
    {
        var pagedListResults = await _authorRepository.GetAllAuthorsAsync(authorParams);

        var authorDtos = _mapper.Map<IEnumerable<AutherDtoForRead>>(pagedListResults);

        var dataShaper = _dataShaper.ShapeData(authorDtos, authorParams.Fields);

        return (authorDtosForRead: dataShaper, metaData: pagedListResults.MetaData);
    }

    public async Task UpdateAuthorAsync(int id, bool trackChanges, AutherDtoForUpdate autherDtoForUpdate)
    {
        var author = await CheckAndReturnAuthor(id, trackChanges);

        _mapper.Map(autherDtoForUpdate, author);

        _authorRepository.UpdateAuthor(author);
    }

    private async Task<Author> CheckAndReturnAuthor(int id, bool trackChanges)
    {
        var author = await _authorRepository.GetAuthorByConditionAsync(trackChanges, a => a.Id.Equals(id));

        if (author is null)
            throw new AuthorNotFoundException(id);

        return author;
    }
}
