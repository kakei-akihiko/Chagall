using Chagall.Domain.Settings;
using Chagall.Usecases;
using Chagall.Views;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Diagnostics;
using System.Linq;
using Windows.ApplicationModel.DataTransfer;

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
                MainModulePath = window.MainModulePath,
                Tag = window.Handle.ToString(),
                Title = window.Text
            });

        foreach (var item in items)
            ViewModel.NavigationItems.Add(item);
    }

    private void ApplyButton_Click(object sender, RoutedEventArgs e)
    {
        var setting = settingRepository.Load();

        windowService.Run(setting.Policies, setting.OtherWindowCommand);
    }

    private void RefreshButton_Click(object sender, RoutedEventArgs e)
    {
        Refresh();
    }

    private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.IsSettingsSelected)
        {
            var path = settingRepository.GetFolderPath();

            Process.Start("EXPLORER.EXE", path);

            Debug.WriteLine("Selected MainNavigationItem. Title: 設定, Path: {0}", path);
            return;
        }
        var item = args.SelectedItem as MainNavigationItem;
        ViewModel.NavigationItem = item;
        Debug.WriteLine("Selected MainNavigationItem. Title: {0}, Tag: {1}", item?.Title, item?.Tag);
    }

    private WindowService windowService = new();
    private readonly SettingRepository settingRepository = new();

    // 右画面

    private void ModulePathCopyButton_Click(object sender, RoutedEventArgs e)
    {
        if (ViewModel.SelecrtedWindowModulePathCopyEnabled)
        {
            var dataPackage = new DataPackage();
            dataPackage.SetText(ViewModel.SelectedWindowModulePath);
            Clipboard.SetContent(dataPackage);
            return;
        }
    }

    private void TitleCopyButton_Click(object sender, RoutedEventArgs e)
    {
        if (ViewModel.SelecrtedWindowModulePathCopyEnabled)
        {
            var dataPackage = new DataPackage();
            dataPackage.SetText(ViewModel.SelectedWindowTitle);
            Clipboard.SetContent(dataPackage);
            return;
        }
    }
}
