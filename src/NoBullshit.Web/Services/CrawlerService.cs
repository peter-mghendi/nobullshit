using NoBullshit.Shared.Models;
using HtmlAgilityPack;
using System.Linq;

namespace NoBullshit.Web.Services;

public class CrawlerService
{
    private const string BaseUrl = "https://nobsgames.stavros.io";

    public static async Task<List<Game>> CrawlAsync(CancellationToken cancellationToken = default)
    {
        List<Game> games = new();

        var androidGames = await CrawlForAndroidGamesAsync(cancellationToken);
        games.AddRange(androidGames);

        var iOSGames = await CrawlForIOSGamesAsync(cancellationToken);
        games.AddRange(iOSGames);

        return games;
    }

    private static async Task<IEnumerable<Game>> CrawlForAndroidGamesAsync(CancellationToken cancellationToken = default)
    {
        HtmlWeb web = new();

        var page = 1;
        bool hasNextPage = true;
        var games = new List<Game>();

        while (page == 1 || hasNextPage)
        {
            HtmlDocument doc = await web.LoadFromWebAsync($"{BaseUrl}/android?page={page}", cancellationToken);
            var nodes = doc.DocumentNode.SelectNodes("//table[@id='apps']//tbody//tr");
            foreach (var node in nodes)
            {
                games.Add(new AndroidGame
                {
                    Name = node.SelectSingleNode(".//td[1]/a").InnerText.Trim().Split('\n')[0],
                    ImageUrl = BaseUrl + node.SelectSingleNode(".//td[1]/a/img").Attributes["src"].Value,
                    StoreUrl = node.SelectSingleNode(".//td[1]/a").Attributes["href"].Value,
                    Genres = node.SelectSingleNode(".//td[2]").InnerText.Split(", "),
                    Rating = float.Parse(node.SelectSingleNode(".//td[3]").InnerText),
                    Reviews = int.Parse(node.SelectSingleNode(".//td[4]").InnerText.Replace(",", "")),
                    Price = decimal.Parse(node.SelectSingleNode(".//td[5]").GetDataAttribute("order").Value),
                    Added = node.SelectSingleNode(".//td[6]").InnerText.Trim()
                });
            }
            var arrow = doc.DocumentNode.SelectSingleNode("//ul[contains(concat(' ', normalize-space(@class), ' '), ' pagination ')]/li[last()]");
            hasNextPage = !arrow.Attributes["class"].Value.Contains("disabled");
            page++;
        }

        return games;
    }

    private static async Task<IEnumerable<Game>> CrawlForIOSGamesAsync(CancellationToken cancellationToken = default)
    {
        HtmlWeb web = new();

        var page = 1;
        bool hasNextPage = true;
        var games = new List<Game>();

        while (page == 1 || hasNextPage)
        {
            HtmlDocument doc = await web.LoadFromWebAsync($"{BaseUrl}/ios?page={page}", cancellationToken);
            var nodes = doc.DocumentNode.SelectNodes("//table[@id='apps']//tbody//tr");
            foreach (var node in nodes)
            {
                games.Add(new IOSGame
                {
                    Name = node.SelectSingleNode(".//td[1]/a").InnerText.Trim().Split('\n')[0],
                    ImageUrl = BaseUrl + node.SelectSingleNode(".//td[1]/a/img").Attributes["src"].Value,
                    StoreUrl = node.SelectSingleNode(".//td[1]/a").Attributes["href"].Value,
                    Genres = node.SelectSingleNode(".//td[2]").InnerText.Split(", "),
                    Rating = float.Parse(node.SelectSingleNode(".//td[3]").InnerText),
                    Reviews = int.Parse(node.SelectSingleNode(".//td[4]").InnerText.Replace(",", "")),
                    Price = decimal.Parse(node.SelectSingleNode(".//td[5]").GetDataAttribute("order").Value),
                    Added = node.SelectSingleNode(".//td[6]").InnerText.Trim()
                });
            }
            var arrow = doc.DocumentNode.SelectSingleNode("//ul[contains(concat(' ', normalize-space(@class), ' '), ' pagination ')]/li[last()]");
            hasNextPage = !arrow.Attributes["class"].Value.Contains("disabled");
            page++;
        }
        
        return games;
    }
}