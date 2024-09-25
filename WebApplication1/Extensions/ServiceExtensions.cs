using AspNetCoreRateLimit;
using Entities.DataTransferObjects.AutherDataTransferObjects;
using Entities.DataTransferObjects.BookDataTransferObjects;
using Entities.DataTransferObjects.CategoryDataTransferObjects;
using Entities.DataTransferObjects.CustomerDataTransferObjects;
using Entities.DataTransferObjects.OrderDataTransferObjects;
using Entities.DataTransferObjects.OrderDetailDataTransferObjects;
using Entities.DataTransferObjects.OrderDetailsDataTransferObjects;
using Entities.DataTransferObjects.PublisherDataTransferObjects;
using Microsoft.EntityFrameworkCore;
using Presentation.ActionFilters;
using Repositories.Concretes.EfCore;
using Repositories.Contracts;
using Services.Concretes;
using Services.Contracts;

namespace WebApplication1.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureRepositoryContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RepositoryContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("SqlCon")));
        }

        public static void ConfigureBookRepository(this IServiceCollection services) =>
            services.AddScoped<IBookRepository, BookRepository>();

        public static void ConfigureBookService(this IServiceCollection services) =>
            services.AddScoped<IBookService, BookManager>();

        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddSingleton<ILoggerService, LoggerManager>();

        public static void ConfigureActionFilters(this IServiceCollection services)
        {
            services.AddSingleton<LogFilterAttribute>();
            services.AddScoped<ValidationFilterAttribute>();
        }

        public static void ConfigureDataShaper(this IServiceCollection services)
        {
            services.AddScoped<IDataShaper<BookDtoForRead>, DataShaperManager<BookDtoForRead>>();
            services.AddScoped<IDataShaper<BookDtoForReadWithDetails>, DataShaperManager<BookDtoForReadWithDetails>>();
            services.AddScoped<IDataShaper<OrderDetailDtoForDetails>, DataShaperManager<OrderDetailDtoForDetails>>();
            services.AddScoped<IDataShaper<AutherDtoForRead>, DataShaperManager<AutherDtoForRead>>();
            services.AddScoped<IDataShaper<CategoryDtoForRead>, DataShaperManager<CategoryDtoForRead>>();
            services.AddScoped<IDataShaper<CustomerDtoForRead>, DataShaperManager<CustomerDtoForRead>>();
            services.AddScoped<IDataShaper<OrderDtoForRead>, DataShaperManager<OrderDtoForRead>>();
            services.AddScoped<IDataShaper<OrderDetailForRead>, DataShaperManager<OrderDetailForRead>>();
            services.AddScoped<IDataShaper<PublisherDtoForRead>, DataShaperManager<PublisherDtoForRead>>();
        }
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin().
                AllowAnyMethod().
                AllowAnyHeader().
                WithExposedHeaders("X-Pagination")
                );

            });
        }

        public static void ConfigureResponseCaching(this IServiceCollection services) =>
          services.AddResponseCaching();

        public static void ConfigureRateLimit(this IServiceCollection services)
        {
            var rateLimitRules = new List<RateLimitRule>()
            {
                new RateLimitRule()
                {
                    Endpoint="*",
                    Limit=30,
                    Period="1m"
                }
            };

            services.Configure<IpRateLimitOptions>(opt =>
            {
                opt.GeneralRules = rateLimitRules;
            });

            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
        }

        public static void ConfigureAuthorsRepository(this IServiceCollection services) =>
            services.AddScoped<IAuthorRepository, AuthorRepository>();

        public static void ConfigureAuthorsService(this IServiceCollection services) =>
            services.AddScoped<IAuthorService, AuthorManager>();

        public static void ConfigureCategoriesRepository(this IServiceCollection services) =>
            services.AddScoped<ICategoryRepository, CategoryRepository>();

        public static void ConfigureCategoriesService(this IServiceCollection services) =>
            services.AddScoped<ICategoryService, CategoryManager>();

        public static void ConfigureCustomersRepository(this IServiceCollection services) =>
            services.AddScoped<ICustomerRepository, CustomerRepository>();

        public static void ConfigureCustomersService(this IServiceCollection services) =>
            services.AddScoped<ICustomerService, CustomerManager>();

        public static void ConfigureOrdersRepository(this IServiceCollection services) =>
            services.AddScoped<IOrderRepository, OrderRepository>();

        public static void ConfigureOrdersService(this IServiceCollection services) =>
            services.AddScoped<IOrderService, OrderManager>();

        public static void ConfigureOrderDetailsRepository(this IServiceCollection services) =>
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();

        public static void ConfigureOrderDetailsService(this IServiceCollection services) =>
            services.AddScoped<IOrderDetailService, OrderDetailManager>();

        public static void ConfigurePublishersRepository(this IServiceCollection services) =>
            services.AddScoped<IPublisherRepository, PublisherRepository>();

        public static void ConfigurePublishersService(this IServiceCollection services) =>
            services.AddScoped<IPublisherService, PublisherManager>();

        public static void ConfigureEmailCodeRepository(this IServiceCollection services) =>
     services.AddScoped<IEmailCodeRepository, EmailCodeRepository>();

        public static void ConfigureEmailCodeService(this IServiceCollection services) =>
            services.AddScoped<IEmailCodeService, EmailCodeManager>();
    }

}
