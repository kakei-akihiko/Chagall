using System.Runtime.InteropServices;
using Windows.Graphics;

namespace Chagall.Infrastructure.Windows;

[StructLayout(LayoutKind.Sequential)]
public struct WindowInfo
{
    public int cbSize;
    public RectInt32 rcWindow;
    public RectInt32 rcClient;
    public int dwStyle;
    public int dwExStyle;
    public int dwWindowStatus;
    public uint cxWindowBorders;
    public uint cyWindowBorders;
    public short atomWindowType;
    public short wCreatorVersion;
}
