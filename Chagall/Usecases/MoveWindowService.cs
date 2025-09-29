using Chagall.Infrastructure.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chagall.Usecases;

class MoveWindowService
{
    public void SetWidth(IntPtr handle, int width)
    {
        var rect = windowRepository.GetRectangle(handle);
        rect.Width = width;
        windowRepository.SetRectangle(handle, rect);
    }

    private readonly WindowRepository windowRepository = new();
}
