using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HabrParser.Contracts;
using HabrParser.Models;
using HabrParser.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HabrParser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedController : ControllerBase
    {
        private readonly IArticlesService _articlesService;
        private readonly ILogger _logger;
        private readonly IFeedService _feedService;
        private readonly IArticleRepository _articleRepository;

        public FeedController(IArticlesService articlesService,
            ILogger<FeedController> logger,
            IFeedService feedService,
            IArticleRepository articleRepository)
        {
            _articlesService = articlesService;
            _logger = logger;
            _feedService = feedService;
            _articleRepository = articleRepository;
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
            
            return Ok();
        }
    }
}
