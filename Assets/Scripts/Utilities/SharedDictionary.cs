using System.Collections.Generic;

public static class SharedDictionary
{
    public static readonly Dictionary<Direction, string> DirectionDictionary = new Dictionary<Direction, string>()
    {
        {Direction.Null,  "无"},
        {Direction.East,  "东"},
        {Direction.West,  "西"},
        {Direction.South, "南"},
        {Direction.North, "北"}
    };
    
    public static readonly Dictionary<Difficulty, string> DifficultyDictionary = new Dictionary<Difficulty, string>()
    {
        {Difficulty.Easy,   "简单"},
        {Difficulty.Normal, "普通"},
        {Difficulty.Hard,   "困难"},
        {Difficulty.Crazy,  "疯狂"},
    };
}
