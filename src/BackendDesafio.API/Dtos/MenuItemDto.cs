namespace BackendDesafio.API.Dtos;

public record MenuItemDto(string Id, string Name, IList<MenuItemDto> Submenus);