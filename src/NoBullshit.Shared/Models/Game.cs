namespace NoBullshit.Shared.Models;

public abstract partial class Game
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string[]? Genres { get; set; }
    public string? ImageUrl { get; set; }
    public string? StoreUrl { get; set; }
    public float Rating { get; set; }
    public int Reviews { get; set; }
    public decimal Price { get; set; }
    public string? Added { get; set; }
    public string? Platform { get; set; }
}