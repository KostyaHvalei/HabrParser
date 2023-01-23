using Contracts;

namespace HabrParser.Services;

public class ArticlesService : IArticlesService
{
    private readonly IFeedService _feedService;

    public ArticlesService(IFeedService feedService)
    {
        _feedService = feedService;
    }
}