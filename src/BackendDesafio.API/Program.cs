using BackendDesafio.API.Domain.Repositories;
using BackendDesafio.API.Endpoints;
using BackendDesafio.API.Exceptions;
using BackendDesafio.API.Infrastructure.Repositories;
using BackendDesafio.API.Validation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddValidation();
builder.Services.AddExceptionHandling();

//builder.Services.AddSingleton<IMenuItemRepository, MenuItemInMemoryRepository>();
builder.Services.AddSingleton<IMenuItemRepository, MenuItemMongoRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.UseHttpsRedirection();
app.UseExceptionHandler();

app.MapMenuEndpoints();

app.Run();

public partial class Program { }