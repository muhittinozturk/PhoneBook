using Persistence;
using Domain;
using Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceService(builder.Configuration);
builder.Services.AddDomainService();
builder.Services.AddApplicationService();

builder.Services.AddControllers();
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

app.Run();
