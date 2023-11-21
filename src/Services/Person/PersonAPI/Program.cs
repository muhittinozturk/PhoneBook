using Persistence;
using Domain;
using Application;
using Microsoft.Extensions.Configuration;
using PersonAPI.IntegrationEvents.IntegrationEvents;
using EventBus.Abstractions;
using EventBus;
using EventBus.RabbitMQ;
using PersonAPI.IntegrationEvents.Events;
using System;
using Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Contexts;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceService(builder.Configuration);
builder.Services.AddDomainService();
builder.Services.AddApplicationService();

builder.Services.AddControllers();

builder.Services.AddTransient<ReportRequestIntegrationEventHandler>();
builder.Services.AddSingleton<IEventBus>(provider =>
{
    Configuration config = new()
    {
        SubClientAppName = "PersonService",
        Connection = new ConnectionFactory()
        {
            HostName = "rabbitmq",
            Port = 5672
        }
    };
    var logger = provider.GetRequiredService<ILogger<RabbitMQConnection>>();
    return new EventBusRabbitMQ(provider, config, logger);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await Task.Delay(4000);
var eventBus = app.Services.GetRequiredService<IEventBus>();
eventBus.Subscribe<ReportRequestIntegrationEvent, ReportRequestIntegrationEventHandler>();

AddMigration.InitialMigration(app);

app.Run();

public partial class Program
{
}
