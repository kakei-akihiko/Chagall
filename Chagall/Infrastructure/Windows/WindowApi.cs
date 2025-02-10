using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows;

namespace Chagall.Infrastructure.Windows;

public class WindowApi
{
    [DllImport("user32.dll")]
    public static extern IntPtr GetDesktopWindow();

    [DllImport("user32.dll")]
    public static extern IntPtr GetParent(IntPtr handle);

    public delegate bool EnumWindowsEventHandler(IntPtr windowHandle, int param);

    [DllImport("user32.dll")]
    public static extern bool EnumWindows(EnumWindowsEventHandler handler, int param);

    [DllImport("user32.dll")]
    public static extern bool EnumChildWindows(
        IntPtr windowHandle, EnumWindowsEventHandler handler, int param);

    // ウインドーの基本情報

    [DllImport("user32.dll")]
    public static extern int IsWindowVisible(IntPtr windowHandle);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int GetWindowText(
        IntPtr windowHandle, StringBuilder windowText, int maxLength);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int GetWindowTextLength(IntPtr windowHandle);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern void SetWindowText(IntPtr windowHandle, string windowText);

    // ウインドーの位置

    [DllImport("user32.dll")]
    public static extern int GetWindowRect(IntPtr windowHandle, ref Int32Rect rect);

    [DllImport("user32.dll")]
    public static extern int ScreenToClient(IntPtr windowHandle, ref WindowLocation point);

    [DllImport("user32.dll")]
    public static extern int MoveWindow(
        IntPtr windowHandle, int x, int y, int width, int height, bool repaint);

    public const uint SwpNoSize = 0x0001;
    public const uint SwpNoMove = 0x0002;
    public const uint SwpNoZOrder = 0x0004;
    public const uint SwpFrameChanged = 0x0020;
    public const uint SwpShowWindow = 0x0040;

    [DllImport("user32.dll")]
    public static extern int SetWindowPos(
        IntPtr handle, IntPtr handleInsertAfter, int x, int y, int w, int h, uint flags);

    // ウインドーの表示

    public const long ShMaximize = 3;
    public const long ShMinimize = 6;
    public const long ShRestore = 9;

    [DllImport("user32.dll")]
    public static extern long ShowWindow(IntPtr handle, long nCommandShow);

    [DllImport("user32.dll")]
    public static extern int IsZoomed(IntPtr handle);

    [DllImport("user32.dll")]
    public static extern int IsIconic(IntPtr handle);

    // ウインドウスタイル・付加情報

    public const int GwlStyle = -16;
    public const int GwlExStyle = -20;
    public const int GwlUserData = -21;

    public const long WsMaximizeBox = 0x00010000;
    public const long WsMinimizeBox = 0x00020000;
    public const long WsSizeBox = 0x00040000;
    public const long WsExLayered = 0x00080000;
    public const long WsHScroll = 0x00100000;
    public const long WsVScroll = 0x00200000;
    public const long WsBorder = 0x00800000;
    public const long WsDisabled = 0x08000000;
    public const long WsVisible = 0x10000000;

    [DllImport("user32.dll")]
    public static extern bool IsWindowEnabled(IntPtr windowHandle);

    [DllImport("user32.dll")]
    public static extern long SetWindowLong(IntPtr windowHandle, int index, long value);

    [DllImport("user32.dll")]
    public static extern long GetWindowLong(IntPtr windowHandle, int index);

    [DllImport("user32.dll")]
    public static extern bool GetLayeredWindowAttributes(
        IntPtr windowHandle, ref uint color, ref byte value, ref ulong flags);

    public const ulong LwaAlpha = 2;

    [DllImport("user32.dll")]
    public static extern bool SetLayeredWindowAttributes(
        IntPtr windowHandle, uint color, byte value, ulong flags);

    [DllImport("user32.dll")]
    public static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    public static extern IntPtr SetForegroundWindow(
        IntPtr windowHandle);

    [DllImport("user32.dll")]
    public static extern uint GetWindowThreadProcessId(
        IntPtr windowHandle, out int processId);
}
