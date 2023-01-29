using HabrParser.Contracts;
using HabrParser.Models;

namespace HabrParser.Services;

public class ArticlesService : IArticlesService
{
    private readonly IArticleRepository _articleRepository;
    private readonly IFeedService _feedService;
    private readonly IFeedParsingService _feedParsingService;
    private readonly IConfiguration _configuration;

    public ArticlesService(
        IArticleRepository articleRepository,
        IFeedService feedService,
        IFeedParsingService feedParsingService,
        IConfiguration configuration)
    {
        _articleRepository = articleRepository;
        _feedService = feedService;
        _feedParsingService = feedParsingService;
        _configuration = configuration;
    }

    public async Task<int> LoadNewArticlesAsync()
    {
        var lastArticle = await _articleRepository.GetLastAsync();
        var newArticles = new List<Article>();
        var maxPageNumber = Int32.Parse(_configuration.GetSection("MaxHabrFeedPageNumber").Value);
        
        for (int pageNumber = 1; pageNumber <= maxPageNumber; pageNumber++)
        {
            var pageContent = await _feedService.LoadPageAsync(pageNumber);
            var pageArticles = await _feedParsingService.ParseRSSPageAsync(pageContent);

            int lastArticleIdx = pageArticles.IndexOf(lastArticle);
            
            if(lastArticleIdx == -1)
                newArticles.AddRange(pageArticles);
            else
            {
                newArticles.AddRange(pageArticles.GetRange(0, lastArticleIdx));
                break;
            }
        }

        newArticles.Reverse();
        return await _articleRepository.AddArticlesAsync(newArticles);
    }
}