using AutoMapper;
using Entities.Concretes;
using Entities.DataTransferObjects;
using Entities.DataTransferObjects.AutherDataTransferObjects;
using Entities.DataTransferObjects.BookDataTransferObjects;
using Entities.DataTransferObjects.CategoryDataTransferObjects;
using Entities.DataTransferObjects.CustomerDataTransferObjects;
using Entities.DataTransferObjects.EmailCodeDataTransferObjects;
using Entities.DataTransferObjects.OrderDataTransferObjects;
using Entities.DataTransferObjects.OrderDetailDataTransferObjects;
using Entities.DataTransferObjects.OrderDetailsDataTransferObjects;
using Entities.DataTransferObjects.PublisherDataTransferObjects;

namespace WebApplication1.Utilities.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDtoForRead>();
            CreateMap<Book, BookDtoForReadWithDetails>();
            CreateMap<BookDtoForUpdate, Book>();
            CreateMap<BookDtoForInsertion, Book>();

            CreateMap<Author, AutherDtoForRead>();
            CreateMap<AutherDtoForInsertion, Author>();
            CreateMap<AutherDtoForUpdate, Author>();

            CreateMap<Category, CategoryDtoForRead>();
            CreateMap<CategoryDtoForInsertion, Category>();
            CreateMap<CategoryDtoForUpdate, Category>();

            CreateMap<Customer, CustomerDtoForRead>();
            CreateMap<CustomerDtoForInsertion, Customer>();
            CreateMap<CustomerDtoForSignInControl, Customer>();
            CreateMap<CustomerDtoForUpdate, Customer>();
            CreateMap<CustomerDtoForChangePassword, Customer>();

            CreateMap<Order, OrderDetailForRead>();
            CreateMap<OrderDtoForInsertion, Order>();
            CreateMap<OrderDtoForUpdate, Order>();

            CreateMap<OrderDetail, OrderDetailForRead>();
            CreateMap<OrderDetail, OrderDetailDtoForDetails>();
            CreateMap<OrderDetailForInsertion, OrderDetail>();
            CreateMap<OrderDetailForUpdate, OrderDetail>();

            CreateMap<Publisher, PublisherDtoForRead>();
            CreateMap<PublisherDtoForInsertion, Publisher>();
            CreateMap<PublisherDtoForUpdate, Publisher>();

            CreateMap<EmailCode, EmailCodeDtoForRead>();
            CreateMap<EmailCodeFtoForInsertion, EmailCode>();
            CreateMap<EmailCodeDtoForUpdate, EmailCode>();
            
        }
    }
}
