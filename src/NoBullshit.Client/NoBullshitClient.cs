using Flurl;
using Flurl.Http;
using NoBullshit.Shared.Models;

namespace NoBullshit.Client;

public class NoBullshitClient
{
    public string BaseUrl { get; init; }
    public string? UserAgent { get; init; }

    public NoBullshitClient(string baseUrl, string userAgent = "NoBullshit.Client")
    {
        BaseUrl = baseUrl;
        UserAgent = userAgent;
    }

    public async Task<Response<List<Game>>> GetGamesAsync(int page)
    {
        return await BaseUrl.AppendPathSegment("games")
            .SetQueryParam("page", page)
            .WithHeaders(new {
                Accept = "application/json",
                User_Agent = UserAgent
            })
            .GetJsonAsync<Response<List<Game>>>();
            
            // .GetStringAsync();
            // Console.WriteLine(x);
            // return new();
    }
}
