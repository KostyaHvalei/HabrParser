using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HabrParser.Contracts;
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
        private readonly IConfiguration _conf;

        public FeedController(IArticlesService articlesService, ILogger<FeedController> logger)
        {
            _articlesService = articlesService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var articles = _articlesService.ParsePage(1);
            return Ok();
        }
    }
}
