using BackendDesafio.API.Domain.Entities;
using BackendDesafio.API.Dtos;

namespace BackendDesafio.API.Mappers;

public static class MenuMapper
{
    public static List<MenuItemDto> GetMenu(List<MenuItem> menuItems) => BuildTree(null, menuItems);

    // Constrói recursivamente uma estrutura de árvore a partir de uma lista plana de itens de menu.
    private static List<MenuItemDto> BuildTree(int? parentId, List<MenuItem> menuItems)
    {
        var result = new List<MenuItemDto>();

        foreach (var menuItem in menuItems)
        {
            if (menuItem.RelatedId != parentId) continue;

            var tree = BuildTree(menuItem.Id, menuItems);

            var dto = new MenuItemDto(
                menuItem.Id.ToString(),
                menuItem.Name,
                tree);

            result.Add(dto);
        }

        return result;
    }
}