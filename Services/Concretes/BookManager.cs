using AutoMapper;
using Entities.Concretes;
using Entities.DataTransferObjects;
using Entities.DataTransferObjects.BookDataTransferObjects;
using Entities.Exceptions.BadRequestExceptions;
using Entities.Exceptions.NotFoundExceptions;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Services.Contracts;
using System.Dynamic;
using static System.Reflection.Metadata.BlobBuilder;

namespace Services.Concretes
{
    public class BookManager : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IMapper _mapper;
        private readonly IDataShaper<BookDtoForRead> _dataShaper;
        private readonly IDataShaper<BookDtoForReadWithDetails> _dataShaper2;

        public BookManager(IBookRepository bookRepository, IMapper mapper, IDataShaper<BookDtoForRead> dataShaper, IDataShaper<BookDtoForReadWithDetails> dataShaper2, IOrderDetailRepository orderDetailRepository)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _dataShaper = dataShaper;
            _dataShaper2 = dataShaper2;
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task CreateBookAsync(BookDtoForInsertion bookDtoForInsertion)
        {
            var book = _mapper.Map<Book>(bookDtoForInsertion);
            _bookRepository.CreateBook(book);
        }

        public async Task DeleteBookAsync(int id, bool trackChanges)
        {
            var book = await CheckAndReturnBookAsync(id, trackChanges);
            _bookRepository.DeleteBook(book);
        }

        public async Task<(IEnumerable<ExpandoObject> bookDtosForRead, MetaData metaData)> GetAllBooksWithParamsAsync(BookParams bookParams)
        {
            if ((!bookParams.ValidPrice) && ((bookParams.MaxPrice != 0) || (bookParams.MinPrice != 0)))
                throw new PriceOutOfRangeBadRequestException();

            var books = await _bookRepository.GetAllBookAsync(bookParams);
            var bookDtos=_mapper.Map<IEnumerable<BookDtoForRead>>(books);
            var dataShaper = _dataShaper.ShapeData(bookDtos,bookParams.Fields);

            return (bookDtosForRead: dataShaper, metaData: books.MetaData);
        }

        public async Task<(IEnumerable<ExpandoObject> bookDtosForRead, MetaData metaData)> GetBooksWithDetailsAsync(BookParams bookParams)
        {
            var results = await _bookRepository.GetBooksWithDetailsAsync(bookParams);

            var dtos = _mapper.Map<List<BookDtoForReadWithDetails>>(results);

            var dtosWithDetails = dtos.Select(b => new BookDtoForReadWithDetails
            {
                Id = b.Id,
                Stock = b.Stock,
                CategoryName = b.CategoryName,
                PublisherName=b.PublisherName,
                AuthorFirstName=b.AuthorFirstName,
                AuthorLastName=b.AuthorLastName,
                Title = b.Title,
                Price = b.Price,
            }).ToList();

            var dataShaper = _dataShaper2.ShapeData(dtosWithDetails,bookParams.Fields);

            return (bookDtosForRead: dataShaper, metaData: results.MetaData);
        }

        public async Task<List<string>> GetTheMostPopularBooksAsync()
        {
            var results = await _bookRepository.GetTheMostPopularBooksAsync();

            return results;
        }

        public async Task UpdateBookAsync(int id, bool trackChanges, BookDtoForUpdate bookDtoForUpdate)
        {
            var book = await CheckAndReturnBookAsync(id, trackChanges);
            _mapper.Map(bookDtoForUpdate, book);
            _bookRepository.UpdateBook(book);
        }

        private async Task<Book> CheckAndReturnBookAsync(int id, bool trackChanges)
        {
            var book = await _bookRepository.
                GetEntitiesByCondition(trackChanges, b => b.Id.Equals(id)).
                SingleOrDefaultAsync();

            if (book is null)
                throw new BookNotFoundException(id);

            return book;
        }

       
    }
}

