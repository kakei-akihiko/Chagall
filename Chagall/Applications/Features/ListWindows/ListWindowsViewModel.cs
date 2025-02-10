using Chagall.Domain.Features.Windows;
using System.Collections.ObjectModel;

namespace Chagall.Applications.Features.ListWindows;

class ListWindowsViewModel
{
    public ObservableCollection<Window> Windows { get; set; } = new();
    public Window? SelectedWindow { get; set; }
}
