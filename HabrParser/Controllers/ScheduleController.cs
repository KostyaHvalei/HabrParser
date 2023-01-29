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
    public class ScheduleController : ControllerBase
    {
        private readonly IArticlesService _articlesService;

        public ScheduleController(IArticlesService articlesService)
        {
            _articlesService = articlesService;
        }
    }
}
