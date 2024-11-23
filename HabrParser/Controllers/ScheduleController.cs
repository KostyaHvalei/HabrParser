using HabrParser.Contracts;
using HabrParser.Models;
using HabrParser.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Hangfire;
using Hangfire.Storage;

namespace HabrParser.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ScheduleController : ControllerBase
{
	private readonly IArticlesService _articlesService;
	private readonly IHistoryRepository _historyRepository;
	private readonly ILogger<ScheduleController> _logger;

	public ScheduleController(IArticlesService articlesService,
		IHistoryRepository historyRepository,
		ILogger<ScheduleController> logger)
	{
		_articlesService = articlesService;
		_historyRepository = historyRepository;
		_logger = logger;
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
			: Ok("There is no schedule");
	}

	[HttpPost]
	public async Task<IActionResult> CreateOrUpdateSchedule([FromBody] ScheduleDTO scheduleDto)
	{
		if (!ModelState.IsValid)
		{
			_logger.LogError($"Models state is not valid");
			return UnprocessableEntity(ModelState);
		}
            
		RecurringJob.AddOrUpdate("parser",
			() =>  LoadNewArticles(),
			scheduleDto.CronSchedule,
			new RecurringJobOptions
			{
				TimeZone = TimeZoneInfo.Local
			});
            
		_logger.LogInformation($"New schedule: {scheduleDto.CronSchedule}");
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
		_logger.LogInformation($"Schedule was removed");
		return Ok();
	}
}