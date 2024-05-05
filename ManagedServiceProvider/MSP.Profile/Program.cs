using MongoDB.Driver;
using MSP.Profile.Model;
using MSP.Profile.Repository;
using MSP.Profile.SyncCommunication.gRPC;
using MSP.Profile.SyncCommunication.Http;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost"));

builder.Services.AddHttpClient<IHttpCommunicationClient, HttpCommunicationClient>();
builder.Services.AddScoped<IGrpcCommunicationService, GrpcCommunicationService>();

builder.Services.AddSingleton<IMongoDatabase>(serviceProvider =>
{
    var host = builder.Configuration.GetValue<string>("MongoDbSettings:Host");
    var port = builder.Configuration.GetValue<string>("MongoDbSettings:Port");

    var mongoDbClient = new MongoClient($"mongodb://{host}:{port}");
    return mongoDbClient.GetDatabase("MSP-Product");
});

builder.Services.AddSingleton<IMongoDbContext<ProfileEntity>>(serviceProvider =>
{
    var database = serviceProvider.GetService<IMongoDatabase>();
    return new MongoDbContext<ProfileEntity>(database!, "ProductDetails");
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
