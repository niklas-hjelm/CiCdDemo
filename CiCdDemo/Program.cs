using CiCdDemo.Extensions;
using DataAccess.Repositories;
using DataAccess.Repositories.Interfaces;
using Domain.Common;
using Domain.Common.DTOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<DatabaseSettings>(_ => new DatabaseSettings()
{
    ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection")
});
builder.Services.AddScoped<IRepository<PersonDto>, PeopleRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPersonEndpoints();

app.Run();