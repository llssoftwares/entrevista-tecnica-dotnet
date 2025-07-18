using BackendDesafio.API.Domain;
using BackendDesafio.API.Domain.Entities;
using BackendDesafio.API.Domain.Repositories;
using BackendDesafio.API.Dtos;
using BackendDesafio.API.Mappers;
using BackendDesafio.API.Validation;

namespace BackendDesafio.API.Endpoints;

public static class MenuEndpoints
{
    public static IEndpointRouteBuilder MapMenuEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/menu")
            .WithTags("Menu")
            .WithOpenApi();

        group.MapPost("/", async (CreateMenuItemRequest request, IMenuItemRepository repository) =>
        {
            var menuItem = new MenuItem
            {
                Name = request.Name,
                RelatedId = request.RelatedId.ToNullableInt()
            };
                
            var menuItemId = await repository.AddMenuItemAsync(menuItem);
            return Results.Created($"/api/v1/menu/{menuItemId}", menuItemId);
        }).WithValidation<CreateMenuItemRequest>();

        group.MapDelete("/{id:int}", async (int id, IMenuItemRepository repository) =>
        {
            await repository.DeleteMenuItemAsync(id);
            return Results.Ok();
        });

        group.MapGet("/", async (IMenuItemRepository repository) =>
        {
            var menuItems = await repository.GetMenuItemsAsync();
            var menu = MenuMapper.GetMenu([.. menuItems]);
            return Results.Ok(menu);
        });

        return app;
    }
}
