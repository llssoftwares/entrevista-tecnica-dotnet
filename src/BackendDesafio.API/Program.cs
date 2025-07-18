using BackendDesafio.API.Domain.Repositories;
using BackendDesafio.API.Endpoints;
using BackendDesafio.API.Infrastructure.Repositories;
using BackendDesafio.API.Middlewares;
using BackendDesafio.API.Validation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddValidation();

//builder.Services.AddSingleton<IMenuItemRepository, MenuItemInMemoryRepository>();
builder.Services.AddSingleton<IMenuItemRepository, MenuItemMongoRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.UseHttpsRedirection();
app.UseMiddlewares();

app.MapMenuEndpoints();

app.Run();