using Chagall.Usecases;
using Chagall.Views;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Linq;

namespace Chagall;

public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        Refresh();
    }

    public MainViewModel ViewModel { get; } = new MainViewModel();

    public void Refresh()
    {
        ViewModel.NavigationItems.Clear();

        var items = windowService.GetWindows()
            .Select(window => new MainNavigationItem()
            {
                Tag = window.Handle.ToString(),
                Title = window.Text,
                Icon = new SymbolIcon(Symbol.View)
            });

        foreach (var item in items)
            ViewModel.NavigationItems.Add(item);
    }

    private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
    }

    private WindowService windowService = new();

    private void RefreshButton_Click(object sender, RoutedEventArgs e)
    {
        Refresh();
    }
}
