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

        public FeedController(IArticlesService articlesService,
            ILogger<FeedController> logger,
            FeedService feedService)
        {
            _articlesService = articlesService;
            _logger = logger;
            _feedService = feedService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var content = await _feedService.LoadPage(1);
            var articles = await _articlesService.ParseRSSPage(content);
            return Ok();
        }
    }
}
