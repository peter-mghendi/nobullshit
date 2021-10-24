namespace NoBullshit.Shared.Models;

public abstract partial class Game
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Genre { get; set; }
    public string? ImageUrl { get; set; }
    public string? StoreUrl { get; set; }
    public float Rating { get; set; }
    public int Reviews { get; set; }
    public decimal Price { get; set; }
    public string? Added { get; set; }
    public GamePlatform Platform { get; set; }
}