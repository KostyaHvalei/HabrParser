using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HabrParser.Contracts;
using HabrParser.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hangfire;

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

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateSchedule([FromBody] ScheduleDTO scheduleDto)
        {
            RecurringJob.AddOrUpdate("parser", () =>  LoadNewArticles(), scheduleDto.CronSchedule);
            return Ok();
        }

        //Synchronous decorator for LoadNewArticlesAsync from ArticlesService
        private void LoadNewArticles()
        {
            var task = _articlesService.LoadNewArticlesAsync();
            task.Wait();
        }
    }
}
