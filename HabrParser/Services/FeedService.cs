using Contracts;

namespace HabrParser.Services;

public class FeedService : IFeedService
{
    private readonly HttpClient _httpClient;

    public FeedService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<string> Load(int pageNumber)
    {
        var response = await _httpClient.GetAsync($"page{pageNumber}");
        return await response.Content.ReadAsStringAsync();
    }
}