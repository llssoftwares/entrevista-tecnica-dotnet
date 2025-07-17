using BackendDesafio.API.Endpoints;
using BackendDesafio.API.Validation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddValidation();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.UseHttpsRedirection();

app.MapMenuEndpoints();

app.Run();