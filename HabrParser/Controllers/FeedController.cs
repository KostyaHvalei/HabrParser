using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HabrParser.Contracts;
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
        private readonly FeedService _feedService;
        private readonly IArticleRepository _articleRepository;

        public FeedController(IArticlesService articlesService,
            ILogger<FeedController> logger,
            FeedService feedService,
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
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> LoadNewArticles()
        {
            
            return Ok();
        }
    }
}
