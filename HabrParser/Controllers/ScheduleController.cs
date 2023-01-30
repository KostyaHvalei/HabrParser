using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HabrParser.Contracts;
using HabrParser.Models;
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
        private readonly IHistoryRepository _historyRepository;

        public ScheduleController(IArticlesService articlesService,
            IHistoryRepository historyRepository)
        {
            _articlesService = articlesService;
            _historyRepository = historyRepository;
        }

        [HttpGet(Name = "GetCurrentSchedule")]
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
            //TODO: add null check for scheduleDTO
            RecurringJob.AddOrUpdate("parser",
                () =>  LoadNewArticles(),
                scheduleDto.CronSchedule,
                new RecurringJobOptions
                {
                    TimeZone = TimeZoneInfo.Local
                });
            
            return CreatedAtRoute("GetCurrentSchedule",
                new ScheduleDTO{CronSchedule = scheduleDto.CronSchedule});
        }

        //Synchronous decorator for LoadNewArticlesAsync from ArticlesService
        [NonAction]
        public void LoadNewArticles()
        {
            var loadingTask = _articlesService.LoadNewArticlesAsync();
            loadingTask.Wait();
            int countLoaded = loadingTask.Result;
            
            var historyTask = _historyRepository.AddAsync(new LoadInfo
            {
                CountLoaded = countLoaded,
                LoadedAt = DateTime.Now,
                LoadedAutomatically = true
            });
            historyTask.Wait();
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveSchedule()
        {
            RecurringJob.RemoveIfExists("parser");
            return Ok();
        }
    }
}
