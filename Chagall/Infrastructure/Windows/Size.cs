namespace Chagall.Infrastructure.Windows;

public struct Size
{
    public int Width { get; set; }
    public int Height { get; set; }
    public Size(int width, int height)
    {
        Width = width;
        Height = height;
    }
    public override string ToString()
    {
        return $"({Width}, {Height})";
    }
}
