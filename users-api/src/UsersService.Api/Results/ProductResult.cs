using UsersService.Business.Dtos;

namespace UsersService.Api.Results;

public sealed record ProductResult(string Name, string Description, decimal Price)
{
    public static ProductResult Create(ProductsDto dto) => new(dto.Name, dto.Description, dto.Price);
}
