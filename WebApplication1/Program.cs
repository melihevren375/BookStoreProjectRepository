using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Mvc;
using NLog;
using Services.Contracts;
using WebApplication1.Extensions;

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.

builder.Services.AddControllers(
    config =>
    {
        config.RespectBrowserAcceptHeader = true;
        config.ReturnHttpNotAcceptable = true;
    }
    ).AddXmlDataContractSerializerFormatters()
    .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    options.JsonSerializerOptions.MaxDepth = 64; 
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureRepositoryContext(builder.Configuration);
builder.Services.ConfigureDataShaper();
builder.Services.ConfigureBookRepository();
builder.Services.ConfigureBookService();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureAuthorsRepository();
builder.Services.ConfigureAuthorsService();
builder.Services.ConfigureCategoriesRepository();
builder.Services.ConfigureCategoriesService();
builder.Services.ConfigureCustomersRepository();
builder.Services.ConfigureCustomersService();
builder.Services.ConfigureOrdersRepository();
builder.Services.ConfigureOrdersService();
builder.Services.ConfigureOrderDetailsRepository();
builder.Services.ConfigureOrderDetailsService();
builder.Services.ConfigurePublishersRepository();
builder.Services.ConfigurePublishersService();
builder.Services.ConfigureEmailCodeRepository();
builder.Services.ConfigureEmailCodeService();
builder.Services.ConfigureCors();
builder.Services.ConfigureResponseCaching();
builder.Services.ConfigureActionFilters();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddMemoryCache();
builder.Services.ConfigureRateLimit();
builder.Services.AddHttpContextAccessor();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggerService>();

app.ConfigureExceptionHandler(logger);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsProduction())
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseIpRateLimiting();
app.UseCors("CorsPolicy");

app.UseResponseCaching();

app.UseAuthorization();

app.MapControllers();

app.Run();
