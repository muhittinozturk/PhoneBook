using EventBus;
using EventBus.Abstractions;
using EventBus.Event;
using EventBus.RabbitMQ;
using MongoDB.Bson;
using RabbitMQ.Client;
using ReportAPI.Data;
using ReportAPI.Entities;
using ReportAPI.Enums;
using ReportAPI.IntegrationEvents.Events;
using ReportAPI.IntegrationEvents.IntegrationEvents;
using ReportAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddScoped(typeof(ReportDbContext<>));
builder.Services.AddScoped(typeof(IReportRepository<>), typeof(ReportRepository<>));


builder.Services.AddTransient<ReportRequestResultIntegrationEventHandler>();
builder.Services.AddSingleton<IEventBus>(provider =>
{
    Configuration config = new()
    {
        SubClientAppName = "ReportService",
        Connection = new ConnectionFactory()
        {
            HostName = "localhost",
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
await Task.Delay(4000);
var eventBus = app.Services.GetRequiredService<IEventBus>();
eventBus.Subscribe<ReportRequestResultIntegrationEvent, ReportRequestResultIntegrationEventHandler>();

app.MapPost("/api/Report", (HttpContext context, IReportRepository<Report> reportRepository) =>
{
    var reportId = ObjectId.GenerateNewId().ToString();
    var reportRequest = new Report
    {
        Id = reportId,
        Status = ReportStatus.InProgress,
        ResultMessage = "Rapor Oluþturma Ýsteði Yapýldý"
    };

    reportRepository.Add(reportRequest);

    IntegrationEvent reportRequestIntegrationEvent = new ReportRequestIntegrationEvent(reportRequest.Id);

    eventBus.Publish(reportRequestIntegrationEvent);

    return Results.Ok("Rapor oluþturma talebi iletildi.");
});

app.MapGet("/api/Report", (IReportRepository<Report> reportRepository) =>
{
    var reports = reportRepository.GetAll();
    return Results.Ok(reports);
});

app.Run();
public partial class Program
{

}