using BackendDesafio.API.Domain.Entities;

namespace BackendDesafio.API.Domain.Repositories;

public interface IMenuItemRepository
{
    Task<IEnumerable<MenuItem>> GetMenuItemsAsync();

    Task<int> AddMenuItemAsync(string name, string? relatedId);

    Task DeleteMenuItemAsync(int id);
}