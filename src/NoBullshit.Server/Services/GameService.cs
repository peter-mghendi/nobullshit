using Microsoft.EntityFrameworkCore;
using NoBullshit.Shared.Models;
using NoBullshit.Server.Data;

namespace NoBullshit.Server.Services;

public class GameService : IGameService
{
    private readonly NoBullshitContext _context;

    public GameService(NoBullshitContext context)
    {
        _context = context;
    }

    public async Task<List<Game>> ListGamesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Games!.ToListAsync(cancellationToken);
    }

    public async Task ReplaceGamesAsync(IEnumerable<Game> games, CancellationToken cancellationToken = default)
    {
        _context.Games!.RemoveRange(_context.Games);
        await _context.Games.AddRangeAsync(games, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}