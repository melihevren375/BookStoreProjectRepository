using AutoMapper;
using Entities.Concretes;
using Entities.DataTransferObjects.OrderDataTransferObjects;
using Entities.DataTransferObjects.OrderDetailsDataTransferObjects;
using Entities.Exceptions.NotFoundExceptions;
using Entities.RequestFeatures;
using Repositories.Contracts;
using Services.Contracts;
using System.Dynamic;

namespace Services.Concretes
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IMapper _mapper;
        private readonly IDataShaper<OrderDtoForRead> _dataShaper;

        public OrderManager(IOrderRepository orderRepository, IMapper mapper, IDataShaper<OrderDtoForRead> dataShaper, IBookRepository bookRepository, IOrderDetailRepository orderDetailRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _dataShaper = dataShaper;
            _bookRepository = bookRepository;
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task CreateOrderAsync(OrderDtoForInsertion orderDtoForInsertion)
        {
            orderDtoForInsertion.TotalAmount = CalculateTotalAmount(orderDtoForInsertion.BookIds, orderDtoForInsertion.Quantities, orderDtoForInsertion.Id).totalAmount;
            var order = _mapper.Map<Order>(orderDtoForInsertion);
            _orderRepository.CreateOrder(order);

            foreach (var orderDetail in CalculateTotalAmount(orderDtoForInsertion.BookIds, orderDtoForInsertion.Quantities, orderDtoForInsertion.Id).orderDetails)
            {
                _orderDetailRepository.CreateOrderDetail(orderDetail);

                var book = await _bookRepository.GetBookByConditionAsync(true, b => b.Id.Equals(orderDetail.BookId));
                uint currentStock = book.Stock;

                uint newStock = currentStock - Convert.ToUInt32(orderDetail.Quantity);
                book.Stock = newStock;
                _bookRepository.UpdateBook(book);
            }
        }

        public async Task DeleteOrderAsync(int id, bool trackChanges)
        {
            var order = await CheckAndReturnOrder(id, trackChanges);
            _orderRepository.DeleteOrder(order);
        }

        public async Task<(IEnumerable<ExpandoObject> orderDtosForRead, MetaData metaData)> GetAllOrdersAsync(OrderParams orderParams)
        {
            var pagedListResults = await _orderRepository.GetAllOrderAsync(orderParams);

            var orderDtos = _mapper.Map<IEnumerable<OrderDtoForRead>>(pagedListResults);

            var dataShaper = _dataShaper.ShapeData(orderDtos, orderParams.Fields);

            return (orderDtosForRead: dataShaper, metaData: pagedListResults.MetaData);
        }

        public async Task UpdateOrderAsync(int id, bool trackChanges, OrderDtoForUpdate orderDtoForUpdate)
        {
            var author = await CheckAndReturnOrder(id, trackChanges);

            _mapper.Map(orderDtoForUpdate, author);

            _orderRepository.UpdateOrder(author);
        }

        private async Task<Order> CheckAndReturnOrder(int id, bool trackChanges)
        {
            var order = await _orderRepository.GetOrderByConditionAsync(trackChanges, a => a.Id.Equals(id));

            if (order is null)
                throw new OrderNotFoundException(id);

            return order;
        }
        public (decimal totalAmount, List<OrderDetail> orderDetails) CalculateTotalAmount(string values, string quantities, Guid orderId)
        {
            decimal totalAmount = 0;
            List<OrderDetail> orderDetails = new();
            var deger = values.Split(',', StringSplitOptions.RemoveEmptyEntries);
            var deger2 = quantities.Split(',', StringSplitOptions.RemoveEmptyEntries);
            int counter = 0;

            foreach (var item in deger)
            {
                Guid id = Guid.Parse(item);

                var book = _bookRepository.GetBookByConditionAsync(true, b => b.Id.Equals(id));

                var quantity = Convert.ToInt32(deger2[counter]);

                var price = book.Result.Price * quantity;

                var orderDetailForInsertion = new OrderDetailForInsertion()
                {
                    OrderId = orderId,
                    BookId = id,
                    Price = price,
                    Quantity = quantity
                };

                var orderDetail = _mapper.Map<OrderDetail>(orderDetailForInsertion);

                orderDetails.Add(orderDetail);

                totalAmount += price;
                counter++;
            }

            return (totalAmount: totalAmount, orderDetails: orderDetails);
        }

        public async Task<int> DailyTotalOrderCountAsync()
        {
            var result =await _orderRepository.DailyTotalOrderCountAsync();

            return result;
        }

        public async Task<int> WeeklyTotalOrderCountAsync()
        {
            var result = await _orderRepository.WeeklyTotalOrderCountAsync();

            return result;
        }

        public async Task<int> MounthlyTotalOrderCountAsync()
        {
            var result = await _orderRepository.MounhtlyTotalOrderCountAsync();

            return result;
        }
    }
}
