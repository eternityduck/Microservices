using Microsoft.AspNetCore.Http;
using UsersService.Business.Dtos;
using UsersService.Business.Interfaces.Communication;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json.Linq;

namespace UsersService.Business.Communication;
public class ProductsHttpClient : IProductsHttpClient
{
    private readonly HttpClient _internalHttpClient;

    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly UriBuilder _ordersServiceUriBuilder;

    public ProductsHttpClient(HttpClient internalHttpClient, IHttpContextAccessor httpContextAccessor)
    {
        _internalHttpClient = internalHttpClient;
        _httpContextAccessor = httpContextAccessor;

        _ordersServiceUriBuilder = new UriBuilder
        {
            Scheme = _httpContextAccessor.HttpContext.Request.Scheme,
            Host = _httpContextAccessor.HttpContext.Request.Host.Host,
            Path = "products"
        };
        if (_httpContextAccessor.HttpContext.Request.Host.Port.HasValue)
        {
            _ordersServiceUriBuilder.Port = _httpContextAccessor.HttpContext.Request.Host.Port!.Value;
        }
    }

    public async Task<IEnumerable<ProductsDto>> GetUserOrdersAsync(int userId)
    {
        _ordersServiceUriBuilder.Query = QueryHelpers.AddQueryString(string.Empty, "ownerId", userId.ToString());

        var message = new HttpRequestMessage
        {
            Method = new(HttpMethod.Get.ToString()),
            RequestUri = _ordersServiceUriBuilder.Uri
        };

        var response = await _internalHttpClient.SendAsync(message);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"ProductsService request failed. Status code: {response.StatusCode}. Response: {response}.");
        }

        var json = JObject.Parse(await response.Content.ReadAsStringAsync());

        var products = json["_embedded"]!["products"]!.ToObject<IEnumerable<ProductsDto>>()!;

        return products;
    }
}
