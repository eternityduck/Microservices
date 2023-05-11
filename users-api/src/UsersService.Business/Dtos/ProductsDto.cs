namespace UsersService.Business.Dtos;

public sealed record ProductsDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
