using Microsoft.UI.Xaml.Controls;

namespace Chagall.Views;

public class MainNavigationItem
{
    public string? Title { get; set; }
    public IconElement? Icon { get; set; }
    public string? MainModulePath { get; set; }

    public string? Tag { get; set; }
}
