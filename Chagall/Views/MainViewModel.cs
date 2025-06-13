using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;

namespace Chagall.Views;

public class MainViewModel
{
    public ObservableCollection<MainNavigationItem> NavigationItems { get; set; } = [
        new ()
        {
            Title = "ウインドウ1",
            Icon = new SymbolIcon(Symbol.Memo),
            Tag = "windows"
        },
        new ()
        {
            Title = "ウインドウ2",
            Icon = new SymbolIcon(Symbol.View),
            Tag = "windows"
        }
    ];
}
