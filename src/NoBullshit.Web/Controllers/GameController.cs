using Microsoft.AspNetCore.Mvc;
using NoBullshit.Shared.Models;
using NoBullshit.Web.Services;

namespace NoBullshit.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class GamesController : ControllerBase
{
    private readonly ILogger<GamesController> _logger;

    public GamesController(ILogger<GamesController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "Get Games")]
    public async Task<IEnumerable<Game>> Get()
    {
        _logger.LogInformation("GetGames called");

        return await CrawlerService.CrawlAsync();

        // return Enumerable.Range(1, 5).Select(index => new AndroidGame()
        // {
        //     Id = index,
        //     Name = $"Game {index}",
        //     Genre = $"Genre {index}",
        //     ImageUrl = $"https://placehold.it/200x200?text={index}",
        //     StoreUrl = $"https://play.google.com/store/apps/details?id={index}",
        //     Rating = index,
        //     Reviews = 1000,
        //     Price = index * 10,
        //     Added = DateOnly.FromDateTime(DateTime.Now).ToString()
        // })
        // .ToArray();
    }
}
