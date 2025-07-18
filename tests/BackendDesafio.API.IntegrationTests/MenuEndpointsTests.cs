using BackendDesafio.API.Dtos;
using BackendDesafio.API.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Shouldly;
using System.Net;
using System.Net.Http.Json;

namespace BackendDesafio.API.IntegrationTests;

public class MenuEndpointsTests(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client = factory.CreateClient();
    private const string _basePath = "/api/v1/menu";    

    [Fact]
    public async Task CreateItems_GetMenu_TreeShouldBeConstructedCorrectly()
    {
        // Arrange
        var menuItems = GetMenuItemsToCreate();

        for (var i = 0; i < menuItems.Count; i++)
        {
            var item = menuItems[i];
            var postResponse = await _client.PostAsJsonAsync(_basePath, item);
            postResponse.StatusCode.ShouldBe(HttpStatusCode.Created);

            var postResponseDto = (await postResponse.Content.ReadFromJsonAsync<CreateMenuItemResponse>())!;
            postResponseDto.Id.ShouldBe((i + 1).ToString()); //Assert
        }

        // Act
        var getResponse = await _client.GetAsync(_basePath);

        // Assert
        getResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

        var getResponseDto = (await getResponse.Content.ReadFromJsonAsync<List<MenuItemDto>>())!;

        AssertMenuTree(getResponseDto);
    }

    [Fact]
    public async Task CreateItem_DeleteItem_ShouldDeleteItem()
    {
        // Arrange
        var postResponse = await _client.PostAsJsonAsync(_basePath, new CreateMenuItemRequest("Item"));
        var itemId = (await postResponse.Content.ReadFromJsonAsync<CreateMenuItemResponse>())!.Id;

        var initialGetResponse = await _client.GetAsync(_basePath);
        var initialItems = (await initialGetResponse.Content.ReadFromJsonAsync<List<MenuItemDto>>())!;

        initialItems.ShouldContain(x => x.Id == itemId); //Assert

        // Act
        var deleteResponse = await _client.DeleteAsync($"{_basePath}/{itemId}");

        // Assert
        deleteResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

        var finalGetResponse = await _client.GetAsync(_basePath);
        var finalItems = (await finalGetResponse.Content.ReadFromJsonAsync<List<MenuItemDto>>())!;

        finalItems.ShouldNotContain(x => x.Id == itemId);
    }

    [Fact]
    public async Task CreateItem_WithInexistentRelatedId_ShouldReturnError()
    {
        // Arrange
        var invalidRequest = new CreateMenuItemRequest("Item", "999");

        // Act
        var response = await _client.PostAsJsonAsync(_basePath, invalidRequest);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        var errorResponse = (await response.Content.ReadFromJsonAsync<ProblemDetails>())!;

        errorResponse.Type.ShouldBe(nameof(RelatedMenuItemNotFoundException));        
        errorResponse.Detail!.ShouldContain("Related menu item with ID 999 does not exist.");
    }

    [Fact]
    public async Task DeleteItem_WithInexistentId_ShouldReturnError()
    {
        // Act
        var response = await _client.DeleteAsync($"{_basePath}/999");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        var errorResponse = (await response.Content.ReadFromJsonAsync<ProblemDetails>())!;

        errorResponse.Type.ShouldBe(nameof(MenuItemNotFoundException));        
        errorResponse.Detail!.ShouldContain("Menu item with ID 999 does not exist.");
    }

    private static List<CreateMenuItemRequest> GetMenuItemsToCreate() =>
    [
        new("Eletrodomésticos"),
        new("Televisores", "1"),
        new("LCD", "2"),
        new("110", "3"),
        new("220", "3"),
        new("Plasma", "2"),
        new("Informática"),
        new("Computadores", "7"),
        new("Apple", "8"),
        new("MacBook", "9"),
        new("Cabos", "10"),
        new("iMac", "9")
    ];

    private static void AssertMenuTree(List<MenuItemDto> menu)
    {
        menu[0].Id.ShouldBe("1");
        menu[0].Name.ShouldBe("Eletrodomésticos");
        menu[0].Submenus.ElementAt(0).Id.ShouldBe("2");
        menu[0].Submenus.ElementAt(0).Name.ShouldBe("Televisores");
        menu[0].Submenus.ElementAt(0).Submenus.ElementAt(0).Id.ShouldBe("3");
        menu[0].Submenus.ElementAt(0).Submenus.ElementAt(0).Name.ShouldBe("LCD");
        menu[0].Submenus.ElementAt(0).Submenus.ElementAt(0).Submenus.ElementAt(0).Id.ShouldBe("4");
        menu[0].Submenus.ElementAt(0).Submenus.ElementAt(0).Submenus.ElementAt(0).Name.ShouldBe("110");
        menu[0].Submenus.ElementAt(0).Submenus.ElementAt(0).Submenus.ElementAt(1).Id.ShouldBe("5");
        menu[0].Submenus.ElementAt(0).Submenus.ElementAt(0).Submenus.ElementAt(1).Name.ShouldBe("220");
        menu[0].Submenus.ElementAt(0).Submenus.ElementAt(1).Id.ShouldBe("6");
        menu[0].Submenus.ElementAt(0).Submenus.ElementAt(1).Name.ShouldBe("Plasma");
        menu[1].Id.ShouldBe("7");
        menu[1].Name.ShouldBe("Informática");
        menu[1].Submenus.ElementAt(0).Id.ShouldBe("8");
        menu[1].Submenus.ElementAt(0).Name.ShouldBe("Computadores");
        menu[1].Submenus.ElementAt(0).Submenus.ElementAt(0).Id.ShouldBe("9");
        menu[1].Submenus.ElementAt(0).Submenus.ElementAt(0).Name.ShouldBe("Apple");
        menu[1].Submenus.ElementAt(0).Submenus.ElementAt(0).Submenus.ElementAt(0).Id.ShouldBe("10");
        menu[1].Submenus.ElementAt(0).Submenus.ElementAt(0).Submenus.ElementAt(0).Name.ShouldBe("MacBook");
        menu[1].Submenus.ElementAt(0).Submenus.ElementAt(0).Submenus.ElementAt(0).Submenus.ElementAt(0).Id.ShouldBe("11");
        menu[1].Submenus.ElementAt(0).Submenus.ElementAt(0).Submenus.ElementAt(0).Submenus.ElementAt(0).Name.ShouldBe("Cabos");
        menu[1].Submenus.ElementAt(0).Submenus.ElementAt(0).Submenus.ElementAt(1).Id.ShouldBe("12");
        menu[1].Submenus.ElementAt(0).Submenus.ElementAt(0).Submenus.ElementAt(1).Name.ShouldBe("iMac");
    }
}