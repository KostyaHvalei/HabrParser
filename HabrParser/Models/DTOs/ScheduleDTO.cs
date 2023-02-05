using System.ComponentModel.DataAnnotations;

namespace HabrParser.Models.DTOs;

public class ScheduleDTO
{
    [Required(ErrorMessage = "CronSchedule is required")]
    public string CronSchedule { get; set; }
}