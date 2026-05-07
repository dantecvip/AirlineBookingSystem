using AirlineBookingSystem.Bookings.Application.Consumers;
using AirlineBookingSystem.Bookings.Application.Handlers;
using AirlineBookingSystem.Bookings.Core.Repositories;
using AirlineBookingSystem.Bookings.Infrastructure.Repositories;
using AirlineBookingSystem.BuildingBlocks.Common;
using MassTransit;
using Microsoft.Data.SqlClient;
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
    typeof(CreateBookingHandler).Assembly,
    typeof(GetBookingHandler).Assembly
};

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));

// Application Services
builder.Services.AddScoped<IBookingRepository, BookingRepository>();

// MassTransit
builder.Services.AddMassTransit(config =>
{
    // Mark this as consumer
    config.AddConsumer<NotificationEventConsumer>();

    config.UsingRabbitMq((ct, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:Host"], builder.Configuration["EventBusSettings:VirtualHost"], h =>
        {
            h.Username(builder.Configuration["EventBusSettings:User"] ?? "guest");
            h.Password(builder.Configuration["EventBusSettings:Password"] ?? "guest");
        });
        cfg.ReceiveEndpoint(EventBusConstant.NotificationSentQueue, c =>
        {
            c.ConfigureConsumer<NotificationEventConsumer>(ct);
        });
    });
});

// Add Sql Connection
builder.Services.AddScoped<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

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
