using Chagall.AppData;
using Chagall.Domain.Settings;
using Chagall.Usecases;
using Chagall.Views;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Diagnostics;
using System.Linq;
using Windows.ApplicationModel.DataTransfer;
using Microsoft.UI.Windowing;
using Chagall.ExMethods;
using Chagall.AppData.State;

namespace Chagall;

public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        appState = stateRepository.Load();

        appWindow = this.GetAppWindow();

        RestoreWindowLocation(appState.MainWindow.Location);

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

        var homeItem = new MainNavigationItem() { Icon = new(Symbol.Home), Title = "ホーム", IsHome = true };
        ViewModel.NavigationItems.Add(homeItem);

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
        var item = args.SelectedItem as MainNavigationItem;
        ViewModel.NavigationItem = item;
        Debug.WriteLine("Selected MainNavigationItem. Title: {0}, Tag: {1}", item?.Title, item?.Tag);
    }

    private WindowService windowService = new();

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

    private void SettingButton_Click(object sender, RoutedEventArgs e)
    {
        AppDataFolder.RunExplorer();
    }

    //
    // このウインドウ自体の変数や処理
    //

    private AppState appState;
    private AppWindow appWindow;
    private readonly SettingRepository settingRepository = new();
    private readonly StateRepository stateRepository = new();

    // ウインドウの位置・大きさが復元可能かどうか取得
    private bool IsValid(RectangleState? windowRect)
    {
        var workArea = DisplayArea.GetFromWindowId(appWindow.Id, DisplayAreaFallback.Primary).WorkArea;

        return windowRect != null
            && windowRect.X > workArea.X - 10
            && windowRect.Y > workArea.Y - 10
            && windowRect.Width <= workArea.Width
            && windowRect.Height <= workArea.Height
            && windowRect.X < workArea.X + workArea.Width - 100
            && windowRect.Y < workArea.Y + workArea.Height - 100;
    }

    // ウインドウが閉じられたとき
    private void Window_Closed(object sender, WindowEventArgs args)
    {
        if (this.IsMaximized() == false)
        {
            var p = appWindow.Position;
            var s = appWindow.Size;
            var location = new RectangleState(p.X, p.Y, s.Width, s.Height);
            var windowState = new MainWindowState(location);
            var appState = new AppState(windowState);
            stateRepository.Save(appState);
        }
    }

    private void RestoreWindowLocation(RectangleState? location)
    {
        if (location != null && IsValid(location))
            appWindow.MoveAndResize(new(location.X, location.Y, location.Width, location.Height));
        else
            appWindow.Resize(new Windows.Graphics.SizeInt32(800, 600));
    }
}
