using NoBullshit.Web.Services;

namespace NoBullshit.Web.Infrastructure;

public sealed class GameCacheUpdateService : IHostedService, IDisposable
{
    private readonly ILogger<GameCacheUpdateService> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    private Timer? _timer;

    public GameCacheUpdateService(
        ILogger<GameCacheUpdateService> logger,
        IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting game cache update service");
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(5));
        return Task.CompletedTask;
    }

    public async void DoWork(object? state)
    {
        _logger.LogInformation("Updating game cache");
        using var scope = _serviceScopeFactory.CreateScope();
        var gameService = scope.ServiceProvider.GetRequiredService<IGameService>();
        var newGames = await CrawlerService.CrawlAsync();
        await gameService.ReplaceGamesAsync(newGames);
        _logger.LogInformation($"Success! {newGames.Count} games inserted.");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping game cache update service");
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose() => _timer?.Dispose();
}