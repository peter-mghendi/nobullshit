using NoBullshit.Shared.Models;

namespace NoBullshit.Web.Services;

public class GameService
{
    private readonly ILogger<GameService> _logger;
    private readonly List<Game> _games = new();

    public GameService(ILogger<GameService> logger)
    {
        _logger = logger;
    }

    public Task Add(Game game) {
        _games.Add(game);
        _logger.LogInformation($"Game {game.Id} added");
        return Task.CompletedTask;
    }
}