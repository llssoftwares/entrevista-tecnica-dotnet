using BackendDesafio.API.Dtos;
using BackendDesafio.API.Validation;
using FluentValidation;

namespace BackendDesafio.API.Endpoints;

public static class MenuEndpoints
{
    public static IEndpointRouteBuilder MapMenuEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/menu")
            .WithTags("Menu")
            .WithOpenApi();

        group.MapPost("/", async (CreateMenuItemRequest request, IValidator<CreateMenuItemRequest> validator) =>
        {
            var menuItemId = "1";
            return Results.Created($"/api/v1/menu/{menuItemId}", menuItemId);
        }).WithValidation<CreateMenuItemRequest>();

        group.MapDelete("/{id:int}", async (int id) =>
        {
            return Results.Ok();
        });

        group.MapGet("/", async () =>
        {
            return Results.Ok();
        });

        return app;
    }
}
