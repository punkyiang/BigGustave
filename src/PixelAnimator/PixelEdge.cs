namespace PixelAnimator;

[Flags]
public enum PixelEdge
{
    Up = 1 << 0,
    Left = 1 << 1,
    Down = 1 << 2,
    Right = 1 << 3
};