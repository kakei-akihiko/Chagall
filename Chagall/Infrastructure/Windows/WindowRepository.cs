using System;
using System.Collections.Generic;
using System.Windows;
using System.Text;

namespace Chagall.Infrastructure.Windows;

public class WindowRepository
{
    public List<IntPtr> EnumWindowHandles()
    {
        var handles = new List<IntPtr>();

        WindowApi.EnumWindows((handle, param) =>
        {
            handles.Add(handle);
            return true;
        }, 0);

        return handles;
    }

    public IntPtr GetDesktopHandle() => WindowApi.GetDesktopWindow();

    public int GetProcessId(IntPtr handle)
    {
        WindowApi.GetWindowThreadProcessId(handle, out int id);
        return id;
    }

    public Int32Rect GetRectangle(IntPtr handle)
    {
        var rect = new Int32Rect();

        WindowApi.GetWindowRect(handle, ref rect);

        var size = new WindowSize(rect.Width - rect.X, rect.Height - rect.Y);

        var point = new WindowLocation(rect.X, rect.Y);

        var desktopHandle = GetDesktopHandle();

        if (handle != IntPtr.Zero && handle != desktopHandle)
        {
            var parentHandle = WindowApi.GetParent(handle);

            if (parentHandle != IntPtr.Zero && parentHandle != desktopHandle)
                WindowApi.ScreenToClient(parentHandle, ref point);
        }

        rect.X = point.X;
        rect.Y = point.Y;
        rect.Width = size.Width;
        rect.Height = size.Height;
        return rect;
    }

    public void SetRectangle(IntPtr handle, Int32Rect value)
    {
        WindowApi.MoveWindow(handle, value.X, value.Y, value.Width, value.Height, true);
    }

    public Size GetSize(IntPtr handle)
    {
        var rect = new Int32Rect();

        WindowApi.GetWindowRect(handle, ref rect);

        return new Size(rect.Width - rect.X, rect.Height - rect.Y);
    }

    public string GetText(IntPtr handle)
    {
        var length = WindowApi.GetWindowTextLength(handle);
        if (length <= 0) return string.Empty;

        var builder = new StringBuilder(length);

        WindowApi.GetWindowText(handle, builder, length + 1);

        return builder.ToString();
    }

    public bool IsVisible(IntPtr handle) =>
        WindowApi.IsWindowVisible(handle) != 0;
}
