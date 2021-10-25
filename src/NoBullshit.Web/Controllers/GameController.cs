using Microsoft.AspNetCore.Mvc;
using NoBullshit.Shared.Models;
using NoBullshit.Web.Services;

namespace NoBullshit.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class GamesController : ControllerBase
{
    private readonly ILogger<GamesController> _logger;
    private readonly IGameService _gameService;

    public GamesController(ILogger<GamesController> logger, IGameService gameService)
    {
        _logger = logger;
        _gameService = gameService;
    }

    [HttpGet(Name = "Get Games")]
    public async Task<IEnumerable<Game>> Get()
    {
        _logger.LogInformation("GetGames called");
        return await _gameService.ListGamesAsync();        
    }
}
