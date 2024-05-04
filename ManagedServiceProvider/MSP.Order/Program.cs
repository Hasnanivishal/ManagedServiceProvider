using MongoDB.Driver;
using MSP.Order.Model;
using MSP.Order.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<IMongoDatabase>(serviceProvider =>
{
    var host = builder.Configuration.GetValue<string>("MongoDbSettings:Host");
    var port = builder.Configuration.GetValue<string>("MongoDbSettings:Port");

    var mongoDbClient = new MongoClient($"mongodb://{host}:{port}");
    return mongoDbClient.GetDatabase("MSP-Order");
});

builder.Services.AddSingleton<IMongoDbContext<OrderEntity>>(serviceProvider =>
{
    var database = serviceProvider.GetService<IMongoDatabase>();
    return new MongoDbContext<OrderEntity>(database!, "OrderDetails");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();