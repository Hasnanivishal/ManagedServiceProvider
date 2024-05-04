using MassTransit;
using MSP.Coupon.Service;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();

    var entryAssembly = Assembly.GetEntryAssembly();

    x.AddConsumers(entryAssembly);

    x.UsingRabbitMq((context, configurator) =>
    {
        var host = builder.Configuration.GetValue<string>("RabbitMqSettings:Host");
        configurator.Host(host);

        // configurator.ConfigureEndpoints(context);

        // Define a seperate queue for Pub-Sub model
        configurator.ReceiveEndpoint("Coupons-Queue", (c) =>
        {
            c.Consumer<OrderNotificationConsumer>();
        });
    });
});


// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGrpcService<CouponService>();

app.Run();
