using Chagall.Domain.Settings;
using Chagall.Usecases;
using System.Windows;

namespace Chagall.Applications.Features.ListWindows;

/// <summary>
/// ListWindowsWindow.xaml の相互作用ロジック
/// </summary>
public partial class ListWindowsWindow : Window
{
    public ListWindowsWindow()
    {
        InitializeComponent();

        foreach (var item in windowService.GetWindows())
            viewModel.Windows.Add(item);

        this.DataContext = viewModel;
    }

    private ListWindowsViewModel viewModel = new();
    private WindowService windowService = new();
    private readonly SettingRepository settingRepository = new();

    private void RunButton_Click(object sender, RoutedEventArgs e)
    {
        var setting = settingRepository.Load();

        windowService.Run(setting.Policies, setting.OtherWindowCommand);
    }

    private void OpenSettingsFolderButton_Click(object sender, RoutedEventArgs e)
    {
        var path = settingRepository.GetFolderPath();

        System.Diagnostics.Process.Start("EXPLORER.EXE", path);
    }

    private void copyTitleButton_Click(object sender, RoutedEventArgs e)
    {
        if (viewModel.SelectedWindow == null)
        {
            MessageBox.Show("ウインドウが選択されていません。");
            return;
        }
        else
        {
            if (!string.IsNullOrEmpty(viewModel.SelectedWindow?.Text))
                Clipboard.SetText(viewModel.SelectedWindow.Text);
        }
    }

    private void copyMainModulePathButton_Click(object sender, RoutedEventArgs e)
    {
        if (viewModel.SelectedWindow == null)
        {
            MessageBox.Show("ウインドウが選択されていません。");
            return;
        }
        else
        {
            var path = viewModel.SelectedWindow?.MainModulePath;
            if (!string.IsNullOrEmpty(path))
                Clipboard.SetText(path);
        }
    }
}
