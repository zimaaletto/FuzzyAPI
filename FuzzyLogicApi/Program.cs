using Persistence;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.Interfaces;
using Domain.RepositoryInterfaces;
using Persistence.Repositories;
using AutoMapper;
using Services.Mappers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("FuzzyConnectionString");

builder.Services.AddDbContext<FuzzyContext>(x => x.UseLazyLoadingProxies().UseSqlServer(connectionString));

builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(provider => new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new MapperProfile());
    cfg.AllowNullCollections = true;
}).CreateMapper());

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
