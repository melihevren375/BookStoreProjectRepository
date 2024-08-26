using Entities.DataTransferObjects.AutherDataTransferObjects;
using Entities.RequestFeatures;
using System.Dynamic;

namespace Services.Contracts
{
    public interface IAuthorService
    {
        Task CreateAuthorAsync(AutherDtoForInsertion autherDtoForInsertion);
        Task UpdateAuthorAsync(int id, bool trackChanges, AutherDtoForUpdate autherDtoForUpdate);
        Task DeleteAuthorAsync(int id, bool trackChanges);
        Task<(IEnumerable<ExpandoObject> authorDtosForRead, MetaData metaData)> GetAllAuthorsAsync(AuthorParams authorParams);
    }
}
