using DocPortal.Application.Services.Processing;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocPortal.Api.Controllers;

[Route("api/[controller]")]
public class StatisticsController(IStatisticsService statisticsService) : _ApiController
{
  [AllowAnonymous]
  [HttpGet("monthly")]
  public IActionResult GetDaily([FromQuery] int? year)
  {
    statisticsService.GetDailyDocumentCount(year ?? default);

    return Ok(statisticsService.GetMonthlyDocumentCount(year ?? default));
  }
}
