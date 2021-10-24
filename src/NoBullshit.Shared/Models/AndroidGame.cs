using static NoBullshit.Shared.Models.Game.GamePlatform;

namespace NoBullshit.Shared.Models;

public class AndroidGame : Game
{
    public new readonly GamePlatform Platform = Android;
}
