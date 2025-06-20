using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Chagall.Views;

public class MainViewModel : INotifyPropertyChanged
{
    public ObservableCollection<MainNavigationItem> NavigationItems { get; set; } = [];

    public MainNavigationItem? NavigationItem
    {
        get => _navigationItem;
        set
        {
            if (_navigationItem != value)
            {
                _navigationItem = value;
                PropertyChanged?.Invoke(this, new(nameof(NavigationItem)));
                PropertyChanged?.Invoke(this, new(nameof(SelectedWindowTitle)));
                PropertyChanged?.Invoke(this, new(nameof(SelecrtedWindowTitleCopyEnabled)));
                PropertyChanged?.Invoke(this, new(nameof(SelectedWindowModulePath)));
                PropertyChanged?.Invoke(this, new(nameof(SelecrtedWindowModulePathCopyEnabled)));
            }
        }
    }

    public string SelectedWindowTitle =>
        NavigationItem == null ? string.Empty
        : NavigationItem.Title ?? "（取得できませんでした）";

    public bool SelecrtedWindowTitleCopyEnabled =>
        !string.IsNullOrEmpty(NavigationItem?.Title);

    public string SelectedWindowModulePath =>
        NavigationItem == null ? string.Empty
        : NavigationItem.MainModulePath ?? "（取得できませんでした）";

    public bool SelecrtedWindowModulePathCopyEnabled =>
        !string.IsNullOrEmpty(NavigationItem?.MainModulePath);

    private MainNavigationItem? _navigationItem = null;

    public event PropertyChangedEventHandler? PropertyChanged;
}
