using ReportAPI.Data;
using ReportAPI.Entities;
using ReportAPI.Enums;
using ReportAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddScoped(typeof(ReportDbContext<>));
builder.Services.AddScoped(typeof(IReportRepository<>), typeof(ReportRepository<>));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


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

    return Results.Ok("Rapor talebi oluþturuldu.");
});

app.MapGet("/getReports", (IReportRepository<Report> reportRepository) =>
{
    var reports = reportRepository.GetAll();
    return Results.Ok(reports);
});

app.Run();
