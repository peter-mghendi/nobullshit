using NoBullshit.Shared.Models;

namespace NoBullshit.Server.Services;

public interface IGameService
{
    Task<List<Game>> ListGamesAsync(CancellationToken cancellationToken = default);
    Task ReplaceGamesAsync(IEnumerable<Game> games, CancellationToken cancellationToken = default);
}
