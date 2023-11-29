public static class PlayerRealWorldDirection
{
    public enum Direction
    {
        Null,
        East,
        West,
        South,
        North
    };
    
    private static Direction _currentDirection = Direction.Null;

    public static Direction CurrentDirection
    {
        get => _currentDirection;
        set => _currentDirection = value;
    }

    public static void SetDirection(Direction newDirection)
    {
        _currentDirection = newDirection;
    }
}
