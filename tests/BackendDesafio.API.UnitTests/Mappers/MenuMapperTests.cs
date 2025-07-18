using BackendDesafio.API.Domain.Entities;
using BackendDesafio.API.Mappers;
using Shouldly;

namespace BackendDesafio.API.UnitTests.Mappers;

public class MenuMapperTests
{
    [Fact]
    public void EmptyList_ShouldBeEmpty()
    {
        MenuMapper.GetMenu([]).ShouldBeEmpty();
    }

    [Fact]
    public void SingleRootItem_ShouldReturnSingleItem()
    {
        var items = new List<MenuItem>
        {
            new() { Id = 1, Name = "Item 1", RelatedId = null }
        };

        var result = MenuMapper.GetMenu(items);

        result.Count.ShouldBe(1);
        result[0].Id.ShouldBe("1");
        result[0].Name.ShouldBe("Item 1");
        result[0].Submenus.ShouldBeEmpty();
    }

    [Fact]
    public void NestedItems_ShouldBuildTree()
    {
        var items = new List<MenuItem>
        {
            new() { Id = 1, Name = "Item 1", RelatedId = null },
            new() { Id = 2, Name = "Item 1.1", RelatedId = 1 },
            new() { Id = 3, Name = "Item 1.2", RelatedId = 1 },
            new() { Id = 4, Name = "Item 1.1.1", RelatedId = 2 }
        };

        var result = MenuMapper.GetMenu(items);

        result.Count.ShouldBe(1);

        result[0].Submenus.Count().ShouldBe(2);
        result[0].Submenus.First().Submenus.Count().ShouldBe(1);
        result[0].Submenus.First().Submenus.First().Name.ShouldBe("Item 1.1.1");
    }

    [Fact]
    public void MultipleRoots_ShouldReturnMultipleTrees()
    {
        var items = new List<MenuItem>
        {
            new() { Id = 1, Name = "Item 1", RelatedId = null },
            new() { Id = 2, Name = "Item 2", RelatedId = null },
            new() { Id = 3, Name = "Item 1.1", RelatedId = 1 }
        };

        var result = MenuMapper.GetMenu(items);

        result.Count.ShouldBe(2);

        result.First(x => x.Name == "Item 1").Submenus.Count().ShouldBe(1);
        result.First(x => x.Name == "Item 2").Submenus.ShouldBeEmpty();
    }
}
