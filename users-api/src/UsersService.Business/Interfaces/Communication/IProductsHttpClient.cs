using UsersService.Business.Dtos;

namespace UsersService.Business.Interfaces.Communication;
public interface IProductsHttpClient
{
    Task<IEnumerable<ProductsDto>> GetUserOrdersAsync(int userId);
}
