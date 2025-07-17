using BackendDesafio.API.Domain.Entities;
using BackendDesafio.API.Domain.Repositories;
using MongoDB.Driver;

namespace BackendDesafio.API.Infrastructure.Repositories;

public class MenuItemMongoRepository : IMenuItemRepository
{
    private readonly IMongoCollection<MenuItem> _collection;

    public MenuItemMongoRepository(IConfiguration configuration)
    {
        var client = new MongoClient(configuration["MongoDB:ConnectionString"]);
        var database = client.GetDatabase(configuration["MongoDB:Database"]);

        _collection = database.GetCollection<MenuItem>("MenuItems");
    }

    public async Task<int> AddMenuItemAsync(string name, string? relatedId)
    {   
        int nextId = await GetNextId();

        var menuItem = new MenuItem
        {
            Id = nextId,
            Name = name,
            RelatedId = int.TryParse(relatedId, out var intRelatedId) 
            ? intRelatedId 
            : null
        };

        await _collection.InsertOneAsync(menuItem);
        return menuItem.Id;
    }

    public async Task DeleteMenuItemAsync(int id)
    {
        var filter = Builders<MenuItem>.Filter.Eq(x => x.Id, id);
        await _collection.DeleteOneAsync(filter);
    }

    public async Task<IEnumerable<MenuItem>> GetMenuItemsAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    private async Task<int> GetNextId()
    {
        var sort = Builders<MenuItem>.Sort.Descending(x => x.Id);
        
        var lastItem = await _collection
            .Find(_ => true)
            .Sort(sort)
            .Limit(1)
            .FirstOrDefaultAsync();

        return (lastItem?.Id ?? 0) + 1;
    }
}