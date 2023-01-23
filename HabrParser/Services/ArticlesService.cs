using HabrParser.Contracts;
using HabrParser.Models;

namespace HabrParser.Services;

public class ArticlesService : IArticlesService
{
    private readonly IFeedService _feedService;

    public ArticlesService(IFeedService feedService)
    {
        _feedService = feedService;
    }

    public async Task<List<Article>> ParsePage(int pageNumber)
    {
        var content = await _feedService.LoadPage(pageNumber);
        return new List<Article>();
    }
}