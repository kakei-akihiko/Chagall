using Chagall.Domain.Features.Windows;
using System.Collections.Generic;

namespace Chagall.Domain.Settings;

internal record AppSetting(List<WindowPolicy> Policies, WindowCommand OtherWindowCommand)
{
}
