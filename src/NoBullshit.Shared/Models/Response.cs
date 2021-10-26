namespace NoBullshit.Shared.Models;

public class Response<T>
{
    public int Pages { get; set; }
    public int CurrentPage { get; set; }
    public string? PrevPageUrl { get; set; }
    public string? NextPageUrl { get; set; }
    public DateTime LastUpdate { get; set; }
    public DateTime NextUpdate { get; set; }
    public T? Data { get; set; }
}