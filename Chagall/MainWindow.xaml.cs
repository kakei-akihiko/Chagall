using Chagall.Usecases;
using Chagall.Views;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Diagnostics;
using System.Linq;

namespace Chagall;

public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
        var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hwnd);
        var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
        appWindow.Resize(new Windows.Graphics.SizeInt32(800, 600));

        Refresh();
    }

    public MainViewModel ViewModel { get; } = new MainViewModel();

    public void Refresh()
    {
        ViewModel.NavigationItems.Clear();

        var items = windowService.GetWindows()
            .Select(window => new MainNavigationItem()
            {
                Icon = new SymbolIcon(Symbol.View),
                MainModulePath = window.MainModulePath,
                Tag = window.Handle.ToString(),
                Title = window.Text
            });

        foreach (var item in items)
            ViewModel.NavigationItems.Add(item);
    }

    private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        var item = args.SelectedItem as MainNavigationItem;
        ViewModel.NavigationItem = item;
        Debug.WriteLine("Selected MainNavigationItem. Title: {0}, Tag: {1}", item?.Title, item?.Tag);
    }

    private WindowService windowService = new();

    private void RefreshButton_Click(object sender, RoutedEventArgs e)
    {
        Refresh();
    }
}
