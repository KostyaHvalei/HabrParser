using HabrParser.Contracts;
using HabrParser.Models;
using HabrParser.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using IHistoryRepository = HabrParser.Contracts.IHistoryRepository;

namespace HabrParser.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FeedController : ControllerBase
{
	private readonly ILogger _logger;
	private readonly IArticleRepository _articleRepository;
	private readonly IArticlesService _articlesService;
	private readonly IHistoryRepository _historyRepository;

	public FeedController(ILogger<FeedController> logger,
		IArticleRepository articleRepository,
		IArticlesService articlesService,
		IHistoryRepository historyRepository)
	{
		_logger = logger;
		_articleRepository = articleRepository;
		_articlesService = articlesService;
		_historyRepository = historyRepository;
	}

	[HttpGet]
	public async Task<IActionResult> GetAllArticles()
	{
		var articles = await _articleRepository.GetAllArticlesAsync();
		return Ok(articles);
	}

	[HttpPost]
	public async Task<IActionResult> LoadNewArticles()
	{
		int countLoaded = await _articlesService.LoadNewArticlesAsync();
		await _historyRepository.AddAsync(new LoadInfo
		{
			CountLoaded = countLoaded,
			LoadedAt = DateTime.Now,
			LoadedAutomatically = false
		});
            
		_logger.LogInformation($"Loaded {countLoaded} Articles");
		return Ok(new LoadNewArticlesResultDTO{CountAdded = countLoaded});
	}
}