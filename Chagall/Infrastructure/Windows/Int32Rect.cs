namespace Chagall.Infrastructure.Windows;

public struct Int32Rect
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public Int32Rect(int x, int y, int width, int height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }
    public override string ToString()
    {
        return $"({X}, {Y}, {Width}, {Height})";
    }
}
