using Arthur.App.Domain;
using Arthur.App.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Arthur.App.Controllers;

[ApiController]
[Route("[controller]")]
public class StatsController : Controller
{
    private readonly IStatsProvider _statsProvider;

    public StatsController(IStatsProvider statsProvider)
    {
        _statsProvider = statsProvider;
    }

    [HttpGet]
    public Stats[] Get() => _statsProvider.Get();
}
