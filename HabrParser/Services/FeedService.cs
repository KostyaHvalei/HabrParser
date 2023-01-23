using HabrParser.Contracts;

namespace HabrParser.Services;

public class FeedService : IFeedService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<FeedService> _logger;

    public FeedService(HttpClient httpClient, ILogger<FeedService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }
    
    public async Task<string> Load(int pageNumber)
    {
        var response = await _httpClient.GetAsync($"page{pageNumber}");
        return await response.Content.ReadAsStringAsync();
    }
}