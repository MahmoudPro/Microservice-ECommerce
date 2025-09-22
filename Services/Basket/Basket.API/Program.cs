using System.Reflection;
using Basket.Application.Mappers;
using Basket.Application.Commands;
using Basket.Core.Repositories;
using Basket.Infrastructure.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddAutoMapper(typeof(BasketMappingProfile).Assembly);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
        Assembly.GetExecutingAssembly(),
        Assembly.GetAssembly(typeof(CreateShoppingCartCommand))
    ));

builder.Services.AddScoped<IBasketRepository, BasketRepository>();




builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
});

builder.Services.AddSwaggerGen();

// Congigure redis settings
builder.Services.AddStackExchangeRedisCache(
    options =>
    {
        options.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseAuthorization();

app.MapControllers();

app.Run();
