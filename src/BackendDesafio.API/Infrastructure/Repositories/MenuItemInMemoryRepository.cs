using BackendDesafio.API.Domain.Entities;
using BackendDesafio.API.Domain.Repositories;

namespace BackendDesafio.API.Infrastructure.Repositories;

public class MenuItemInMemoryRepository : IMenuItemRepository
{
    private readonly List<MenuItem> _menuItems = [];

    public async Task<IEnumerable<MenuItem>> GetMenuItemsAsync()
    {
        var result = _menuItems
            .Select(m => new MenuItem
            {
                Id = m.Id,
                Name = m.Name,
                RelatedId = m.RelatedId
            });

        return await Task.FromResult(result);
    }

    public async Task<int> AddMenuItemAsync(string name, string? relatedId)
    {
        var newId = _menuItems.Count != 0
            ? _menuItems.Max(x => x.Id) + 1
            : 1;

        _menuItems.Add(new MenuItem
        {
            Id = newId,
            Name = name,
            RelatedId = string.IsNullOrEmpty(relatedId)
                ? null
                : int.Parse(relatedId)
        });

        return await Task.FromResult(newId);
    }

    public async Task DeleteMenuItemAsync(int id)
    {
        var menuItem = _menuItems.FirstOrDefault(m => m.Id == id);
        if (menuItem == null) return;

        _menuItems.Remove(menuItem);

        await Task.CompletedTask;
    }
}