using UsersService.Business.Dtos;
using UsersService.Business.Interfaces.Communication;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json.Linq;

namespace UsersService.Business.Communication;
public class ProductsHttpClient : IProductsHttpClient
{
    private readonly HttpClient _internalHttpClient;

    public ProductsHttpClient(HttpClient internalHttpClient)
    {
        _internalHttpClient = internalHttpClient;
    }

    public async Task<IEnumerable<ProductsDto>> GetUserOrdersAsync(int userId)
    {
        var requestPath = "products/search/findAllByOwnerId";
        requestPath = QueryHelpers.AddQueryString(requestPath, "ownerId", userId.ToString());

        var message = new HttpRequestMessage(HttpMethod.Get, requestPath);

        var response = await _internalHttpClient.SendAsync(message);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"ProductsService request failed. Request Uri: {message.RequestUri}. Status code: {response.StatusCode}. Response: {response}.");
        }

        var json = JObject.Parse(await response.Content.ReadAsStringAsync());

        var products = json["_embedded"]!["products"]!.ToObject<IEnumerable<ProductsDto>>()!;

        return products;
    }
}
