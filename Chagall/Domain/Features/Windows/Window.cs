using System;
using System.Windows;

namespace Chagall.Domain.Features.Windows;

class Window
{
    public IntPtr Handle { get; set; }
    public string? MainModulePath { get; set; }
    public int ProcessId { get; set; }
    public Int32Rect Rectangle { get; set; }
    public string? Text { get; set; }
    public bool Visible { get; set; }
}
