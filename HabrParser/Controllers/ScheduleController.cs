using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HabrParser.Contracts;
using HabrParser.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hangfire;
using Hangfire.Storage;

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

        [HttpGet]
        public async Task<IActionResult> GetCurrentSchedule()
        {
            var recurringJob = JobStorage.
                Current.
                GetConnection().
                GetRecurringJobs().
                Find(x => x.Id == "parser");

            return recurringJob != null 
                ? Ok(new ScheduleDTO{CronSchedule = recurringJob.Cron}) 
                : Ok("There is not schedule");
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateSchedule([FromBody] ScheduleDTO scheduleDto)
        {
            RecurringJob.AddOrUpdate("parser",
                () =>  LoadNewArticles(),
                scheduleDto.CronSchedule,
                new RecurringJobOptions
                {
                    TimeZone = TimeZoneInfo.Local
                });
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
