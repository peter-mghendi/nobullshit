namespace NoBullshit.Web.Infrastructure;

public class GameCacheUpdateStatsService
{
    public readonly TimeSpan Period = TimeSpan.FromHours(1);

    public DateTime LastUpdate { get; set; } = DateTime.Now;

    public DateTime NextUpdate => LastUpdate.Add(Period);
}