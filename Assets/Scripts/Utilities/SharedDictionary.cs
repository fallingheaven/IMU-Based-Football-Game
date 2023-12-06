using System.Collections.Generic;

public static class SharedDictionary
{
    public static Dictionary<Direction, string> directionDictionary = new Dictionary<Direction, string>()
    {
        {Direction.Null,  "无"},
        {Direction.East,  "东"},
        {Direction.West,  "西"},
        {Direction.South, "南"},
        {Direction.North, "北"}
    };
}
