using AirlineBookingSystem.BuildingBlocks.Common;
using AirlineBookingSystem.Payments.Application.Consumers;
using AirlineBookingSystem.Payments.Application.Handlers;
using AirlineBookingSystem.Payments.Core.Repositories;
using AirlineBookingSystem.Payments.Infrastructure.Repositories;
using MassTransit;
using Npgsql;
using System.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


// Register MediatR
var assemblies = new Assembly[]
{
    Assembly.GetExecutingAssembly(),
    typeof(ProcessPaymentHandler).Assembly,
    typeof(RefundPaymentHandler).Assembly
};

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));

// Application Services
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();

// MassTransit
builder.Services.AddMassTransit(config =>
{
    // Mark this as consumer
    config.AddConsumer<FlightBookedConsumer>();

    config.UsingRabbitMq((ct, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:Host"], builder.Configuration["EventBusSettings:VirtualHost"], h =>
        {
            h.Username(builder.Configuration["EventBusSettings:User"] ?? "guest");
            h.Password(builder.Configuration["EventBusSettings:Password"] ?? "guest");
        });
        cfg.ReceiveEndpoint(EventBusConstant.FlightBookedQueue, c =>
        {
            c.ConfigureConsumer<FlightBookedConsumer>(ct);
        });
    });
});

// Add Sql Connection
builder.Services.AddScoped<IDbConnection>(sp =>
    new NpgsqlConnection(builder.Configuration.GetConnectionString("PostgresConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
