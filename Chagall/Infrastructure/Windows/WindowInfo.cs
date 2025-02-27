using System.Runtime.InteropServices;
using System.Windows;

namespace Chagall.Infrastructure.Windows;

[StructLayout(LayoutKind.Sequential)]
public struct WindowInfo
{
    public int cbSize;
    public Int32Rect rcWindow;
    public Int32Rect rcClient;
    public int dwStyle;
    public int dwExStyle;
    public int dwWindowStatus;
    public uint cxWindowBorders;
    public uint cyWindowBorders;
    public short atomWindowType;
    public short wCreatorVersion;
}
