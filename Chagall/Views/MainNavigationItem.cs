using Microsoft.UI.Xaml.Controls;

namespace Chagall.Views;

public class MainNavigationItem
{
    public SymbolIcon Icon { get; set; } = new SymbolIcon(Symbol.PreviewLink);

    public bool IsHome { get; set; } = false;

    public string? Title { get; set; }

    public string? MainModulePath { get; set; }

    public string? Tag { get; set; }
}
