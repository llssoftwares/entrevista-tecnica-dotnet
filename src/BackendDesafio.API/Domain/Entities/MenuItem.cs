namespace BackendDesafio.API.Domain.Entities;

public class MenuItem
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int? RelatedId { get; set; }    
}