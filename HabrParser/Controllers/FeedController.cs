using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HabrParser.Contracts;
using HabrParser.Models;
using HabrParser.Models.DTOs;
using HabrParser.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IHistoryRepository = HabrParser.Contracts.IHistoryRepository;

namespace HabrParser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedController : ControllerBase
    {
        private readonly IFeedParsingService _feedParsingService;
        private readonly ILogger _logger;
        private readonly IFeedService _feedService;
        private readonly IArticleRepository _articleRepository;
        private readonly IArticlesService _articlesService;
        private readonly IHistoryRepository _historyRepository;

        public FeedController(IFeedParsingService feedParsingService,
            ILogger<FeedController> logger,
            IFeedService feedService,
            IArticleRepository articleRepository,
            IArticlesService articlesService,
            IHistoryRepository historyRepository)
        {
            _feedParsingService = feedParsingService;
            _logger = logger;
            _feedService = feedService;
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
            return Ok(new LoadNewArticlesResultDTO{CountAdded = countLoaded});
        }
    }
}
