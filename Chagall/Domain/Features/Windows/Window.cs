using System;
using Windows.Graphics;

namespace Chagall.Domain.Features.Windows;

class Window
{
    public IntPtr Handle { get; set; }
    public bool MainModulePathEnabled => _mainModulepath != null;
    public string? MainModulePath
    {
        get => _mainModulepath ?? "（取得できません）";
        set => _mainModulepath = value;
    }
    private string? _mainModulepath = null;
    public int ProcessId { get; set; }
    public RectInt32 Rectangle { get; set; }
    public string? Text { get; set; }
    public bool Visible { get; set; }
}
