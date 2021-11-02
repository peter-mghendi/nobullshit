using Microsoft.AspNetCore.Mvc;
using NoBullshit.Shared.Models;
using NoBullshit.Server.Infrastructure;
using NoBullshit.Server.Services;

namespace NoBullshit.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class GamesController : ControllerBase
{
    private const int NUM = 20;
    private readonly GameCacheUpdateStatsService _statsService;
    private readonly ILogger<GamesController> _logger;
    private readonly IGameService _gameService;

    public GamesController(GameCacheUpdateStatsService statsService, ILogger<GamesController> logger, IGameService gameService)
    {
        _statsService = statsService;
        _logger = logger;
        _gameService = gameService;
    }

    [HttpGet(Name = "Get Games")]
    public async Task<Response<IEnumerable<Game>>> Get([FromQuery] int page)
    {
        page = Math.Clamp(page, 1, int.MaxValue);
        var games = await _gameService.ListGamesAsync();
        var pages = (int)Math.Ceiling((double)(games.Count / NUM));
        var start = ((page - 1) * NUM) + 1;

        return new ()
        {
            CurrentPage = page,
            Pages = pages,
            PrevPageUrl = page > 1 ? $"/games?page={page - 1}" : null,
            NextPageUrl = page < pages ? $"/games?page={page + 1}" : null,
            LastUpdate = _statsService.LastUpdate,
            NextUpdate = _statsService.NextUpdate,
            Data = games.Take(start..(start + NUM))
        };
    }
}
