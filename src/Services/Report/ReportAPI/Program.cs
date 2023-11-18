using EventBus;
using EventBus.Abstractions;
using EventBus.Event;
using EventBus.RabbitMQ;
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
        SubClientAppName = "ReportService"
    };
    return new EventBusRabbitMQ(provider, config);
});



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var eventBus = app.Services.GetRequiredService<IEventBus>();
eventBus.Subscribe<ReportRequestResultIntegrationEvent, ReportRequestResultIntegrationEventHandler>();

app.MapPost("/createReport", (HttpContext context, IReportRepository<Report> reportRepository) =>
{
    var reportId = Guid.NewGuid();
    var reportRequest = new Report
    {
        Id = reportId,
        RequestedAt = DateTime.Now,
        Status = ReportStatus.InProgress
    };

    reportRepository.Add(reportRequest);

    IntegrationEvent reportRequestIntegrationEvent = new ReportRequestIntegrationEvent(reportId);

    eventBus.Publish(reportRequestIntegrationEvent);

    return Results.Ok("Rapor talebi oluþturuldu.");
});

app.MapGet("/getReports", (IReportRepository<Report> reportRepository) =>
{
    var reports = reportRepository.GetAll();
    return Results.Ok(reports);
});

app.Run();
