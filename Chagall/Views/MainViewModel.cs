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
                PropertyChanged?.Invoke(this, new(nameof(SelectedWindowTtitle)));
                PropertyChanged?.Invoke(this, new(nameof(SelectedWindowModulePath)));
            }
        }
    }

    public string? SelectedWindowTtitle =>
        NavigationItem == null ? string.Empty
        : NavigationItem.Title ?? "（取得できませんでした）";

    public string SelectedWindowModulePath =>
        NavigationItem == null ? string.Empty
        : NavigationItem.MainModulePath ?? "（取得できませんでした）";

    private MainNavigationItem? _navigationItem = null;

    public event PropertyChangedEventHandler? PropertyChanged;
}
