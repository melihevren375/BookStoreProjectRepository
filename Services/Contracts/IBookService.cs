using Entities.DataTransferObjects;
using Entities.DataTransferObjects.BookDataTransferObjects;
using Entities.RequestFeatures;
using System.Dynamic;

namespace Services.Contracts
{
    public interface IBookService
    {
        Task CreateBookAsync(BookDtoForInsertion bookDtoForInsertion);
        Task DeleteBookAsync(Guid id, bool trackChanges);
        Task UpdateBookAsync(Guid id, bool trackChanges, BookDtoForUpdate bookDtoForUpdate);
        Task<(IEnumerable<ExpandoObject> bookDtosForRead, MetaData metaData)> GetAllBooksWithParamsAsync(BookParams bookParams);
        Task<(IEnumerable<ExpandoObject> bookDtosForRead, MetaData metaData)> GetBooksWithDetailsAsync(BookParams bookParams);
        Task<List<string>> GetTheMostPopularBooksAsync();
    }
}
