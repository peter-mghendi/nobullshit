using static NoBullshit.Shared.Models.Game.GamePlatform;

namespace NoBullshit.Shared.Models;

public class IOSGame : Game
{
    public override GamePlatform Platform => IOS;
}