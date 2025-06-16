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
            }
        }
    }

    private MainNavigationItem? _navigationItem = null;

    public event PropertyChangedEventHandler? PropertyChanged;
}
