using HabrParser.Contracts;

namespace HabrParser.Services;

public class ArticlesService : IArticlesService
{
    private readonly IArticleRepository _articleRepository;
    private readonly IFeedService _feedService;
    private readonly IFeedParsingService _feedParsingService;

    public ArticlesService(
        IArticleRepository articleRepository,
        IFeedService feedService,
        IFeedParsingService feedParsingService)
    {
        _articleRepository = articleRepository;
        _feedService = feedService;
        _feedParsingService = feedParsingService;
    }

    public Task<int> LoadNewArticlesAsync()
    {
        throw new NotImplementedException();
    }
}