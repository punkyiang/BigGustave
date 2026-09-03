namespace PixelAnimator;

[Flags]
public enum PixelEdge
{
    Up = 1 << 0,
    Left = 1 << 1,
    Down = 1 << 2,
    Right = 1 << 3,
    UpLeft = 1 << 4,
    DownLeft = 1 << 5,
    DownRight = 1 << 6,
    UpRight = 1 << 7,
};