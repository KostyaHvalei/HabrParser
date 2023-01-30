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
    public class HistoryController : ControllerBase
    {
        private readonly IHistoryRepository _historyRepository;

        public HistoryController(IHistoryRepository historyRepository)
        {
            _historyRepository = historyRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetHistory()
        {
            return Ok(await _historyRepository.GetFullHistoryAsync());
        }
    }
}
