using AutoMapper;
using Entities.Concretes;
using Entities.DataTransferObjects.OrderDetailDataTransferObjects;
using Entities.DataTransferObjects.OrderDetailsDataTransferObjects;
using Entities.Exceptions.NotFoundExceptions;
using Entities.RequestFeatures;
using Repositories.Contracts;
using Services.Contracts;
using System.Dynamic;

namespace Services.Concretes
{
    public class OrderDetailManager : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly IDataShaper<OrderDetailForRead> _dataShaper;
        private readonly IDataShaper<OrderDetailDtoForDetails> _dataShaper2;

        public OrderDetailManager(IOrderDetailRepository orderDetailRepository, IMapper mapper, IDataShaper<OrderDetailForRead> dataShaper, IBookRepository bookRepository, IDataShaper<OrderDetailDtoForDetails> dataShaper2)
        {
            _orderDetailRepository = orderDetailRepository;
            _mapper = mapper;
            _dataShaper = dataShaper;
            _bookRepository = bookRepository;
            _dataShaper2 = dataShaper2;
        }

        public async Task CreateOrderDetailAsync(OrderDetailForInsertion orderDetailDtoForInsertion)
        {
            OrderDetail orderDetail = new OrderDetail();
            var ids = orderDetailDtoForInsertion.BookIds.Split(',', StringSplitOptions.RemoveEmptyEntries);
            var quantities = orderDetailDtoForInsertion.Quantities.Split(',', StringSplitOptions.RemoveEmptyEntries);

            if (ids.Count() != quantities.Count())
                throw new Exception($"id ve quantites sayıları uymuyor!");

            for (int i = 0; i < ids.Count(); i++)
            {
                var quantity = Convert.ToInt32(quantities[i]);
                Guid id = Guid.Parse(ids[i]);

                orderDetail.BookId = id;
                orderDetail.Quantity = quantity;

                var book = await _bookRepository.
                    GetBookByConditionAsync(true, b => b.Id.Equals(id));

                orderDetail.Price = quantity * book.Price;
                orderDetail.OrderId = orderDetailDtoForInsertion.OrderId;

                orderDetail = _mapper.Map<OrderDetail>(orderDetailDtoForInsertion);
                _orderDetailRepository.CreateOrderDetail(orderDetail);
            }

            for (int i = 0; i < ids.Count(); i++)
            {
                var book = await _bookRepository.GetBookByConditionAsync(true, b => b.Id.Equals(ids[i]));
                uint currentStock = book.Stock;

                uint newStock = currentStock - Convert.ToUInt32(quantities[i]);
                book.Stock = newStock;
                _bookRepository.UpdateBook(book);
            }

        }

        public async Task DeleteOrderDetailAsync(Guid id, bool trackChanges)
        {
            var orderDetail = await CheckAndReturnOrderDetail(id, trackChanges);

            _orderDetailRepository.DeleteOrderDetail(orderDetail);
        }

        public async Task<(IEnumerable<ExpandoObject> orderDetailDtosForRead, MetaData metaData)> GetAllOrderDetailsAsync(OrderDetailParams orderDetailParams)
        {
            var pagedListResults = await _orderDetailRepository.GetAllOrderDetailAsync(orderDetailParams);

            var orderDetailDtos = _mapper.Map<IEnumerable<OrderDetailForRead>>(pagedListResults);

            var dataShaper = _dataShaper.ShapeData(orderDetailDtos, orderDetailParams.Fields);

            return (orderDetailDtosForRead: dataShaper, metaData: pagedListResults.MetaData);
        }

        public async Task<(IEnumerable<ExpandoObject> orderDetailDtosForDetails, MetaData metaData)> GetAllOrderDetailsWithDetailsAsync(OrderDetailParams orderDetailParams)
        {
            var results = await _orderDetailRepository.GetAllOrderDetailsWithDetailsAsync(orderDetailParams);

            var dtos = _mapper.Map<List<OrderDetailDtoForDetails>>(results);

            var dtosWithDetails = dtos.Select(od => new OrderDetailDtoForDetails()
            {
                BookId = od.BookId,
                Id = od.Id,
                OrderId = od.OrderId,
                Price = od.Price,
                Quantity = od.Quantity,
                BookName = od.BookName,
            }).ToList();

            var dataShaper = _dataShaper2.ShapeData(dtosWithDetails, orderDetailParams.Fields);
            return (orderDetailDtosForDetails: dataShaper, metaData: results.MetaData);
        }

        public async Task UpdateOrderDetailAsync(Guid id, bool trackChanges, OrderDetailForUpdate orderDetailDtoForUpdate)
        {
            var orderDetail = await CheckAndReturnOrderDetail(id, trackChanges);

            _mapper.Map(orderDetailDtoForUpdate, orderDetail);

            _orderDetailRepository.UpdateOrderDetail(orderDetail);
        }

        private async Task<OrderDetail> CheckAndReturnOrderDetail(Guid id, bool trackChanges)
        {
            var orderDetail = await _orderDetailRepository.GetOrderDetailByConditionAsync(trackChanges, a => a.Id.Equals(id));

            if (orderDetail is null)
                throw new OrderDetailNotFoundException(id);

            return orderDetail;
        }

    }
}
