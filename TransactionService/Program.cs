using Microsoft.EntityFrameworkCore;
using TransactionService.Application.Handlers;
using TransactionService.Application.Interfaces;
using TransactionService.Infrastructure.Db;
using TransactionService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// InMemoryDb with DbContextFactory
builder.Services.AddDbContextFactory<TransactionDbContext>(options =>
    options.UseInMemoryDatabase("TransactionDb"));

// Repository + Handler
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<UserLoggedInHandler>();

// Hosted service for RabbitMQ consumer
builder.Services.AddHostedService<RabbitMqHostedService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TRANSACTIONS.API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
