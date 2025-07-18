using BackendDesafio.API.Domain.Entities;
using BackendDesafio.API.Dtos;

namespace BackendDesafio.API.Mappers;

public static class MenuMapper
{
    public static List<MenuItemDto> GetMenu(List<MenuItem> menuItems)
    {
        var dtoById = new Dictionary<int, MenuItemDto>();

        foreach (var item in menuItems)
            dtoById[item.Id] = new MenuItemDto(item.Id.ToString(), item.Name, []);

        var roots = new List<MenuItemDto>();

        foreach (var item in menuItems)
        {
            if (item.RelatedId == null)
                roots.Add(dtoById[item.Id]);
            else if (dtoById.TryGetValue(item.RelatedId.Value, out var parentDto))
                parentDto.Submenus.Add(dtoById[item.Id]);
        }

        return roots;
    }
}