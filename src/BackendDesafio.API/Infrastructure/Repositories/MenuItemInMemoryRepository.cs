using BackendDesafio.API.Domain;
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

    public async Task<int> AddMenuItemAsync(MenuItem menuItem)
    {
        if (menuItem.RelatedId.HasValue && !Exists(menuItem.RelatedId.Value))
            throw new RelatedMenuItemNotFoundException(menuItem.RelatedId.Value);

        menuItem.Id = _menuItems.Count != 0
            ? _menuItems.Max(x => x.Id) + 1
            : 1;

        _menuItems.Add(menuItem);

        return await Task.FromResult(menuItem.Id);
    }

    public async Task DeleteMenuItemAsync(int id)
    {
        if (!Exists(id))
            throw new MenuItemNotFoundException(id);

        var menuItem = _menuItems.FirstOrDefault(m => m.Id == id);
        if (menuItem == null) return;

        _menuItems.Remove(menuItem);

        await Task.CompletedTask;
    }

    private bool Exists(int id)
    {
        return _menuItems.Any(x => x.Id == id);
    }
}