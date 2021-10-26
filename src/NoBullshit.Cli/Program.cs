using NoBullshit.Cli.Commands;
using Spectre.Console.Cli;

var app = new CommandApp();

app.Configure(config =>
{
    config.AddCommand<GetGamesCommand>("get")
        .WithDescription("Get a list of games")
        .WithExample(new[] { "get" })
        .WithExample(new[] { "get", "-p", "2" })
        .WithExample(new[] { "get", "--page", "2" });
});

return app.Run(args);