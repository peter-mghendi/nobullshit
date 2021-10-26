using System.ComponentModel;
using System.Linq;
using NoBullshit.Client;
using Spectre.Console;
using Spectre.Console.Cli;

namespace NoBullshit.Cli.Commands;

public class GetGamesCommand : AsyncCommand<GetGamesCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandOption("-p|--page")]
        [Description("The page to fetch")]
        [DefaultValue(1)]
        public int Page { get; init; }

        public override ValidationResult Validate()
        {
            return Page < 1
                ? ValidationResult.Error("Page must be greater than 0")
                : ValidationResult.Success();
        }
    }

    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        var client = new NoBullshitClient("http://localhost:5141", "NoBullshit.Cli");
        var response = await AnsiConsole.Status()
                    .StartAsync("Fetching...", async ctx => await client.GetGamesAsync(settings.Page));
        
        AnsiConsole.MarkupLine($"Page [bold yellow]{response.CurrentPage}[/] of [bold yellow]{response.Pages}[/]");
        AnsiConsole.MarkupLine($"Last Update: [bold yellow]{response.LastUpdate}[/]");
        AnsiConsole.MarkupLine($"Next Update: [bold yellow]{response.NextUpdate}[/]");
        AnsiConsole.WriteLine();
        
        var table = new Table();
        table.AddColumn(new("[bold yellow]Name[/]"));
        table.AddColumn(new("[bold yellow]Platform[/]"));
        table.AddColumn(new("[bold yellow]Genres[/]"));
        table.AddColumn(new("[bold yellow]Price[/]"));
        table.AddColumn(new("[bold yellow]Rating[/]"));
        table.AddColumn(new("[bold yellow]Reviews[/]"));

        response.Data?.ForEach(game =>
            table.AddRow(
                game.Name ?? string.Empty,
                game.Platform ?? string.Empty,
                string.Join(", ", game.Genres ?? Array.Empty<string>()),
                $"${game.Price}",
                game.Rating.ToString(),
                game.Reviews.ToString())
            );

        AnsiConsole.Write(table);
        return 0;
    }
}