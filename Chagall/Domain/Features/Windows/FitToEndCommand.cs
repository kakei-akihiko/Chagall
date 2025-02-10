using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chagall.Domain.Features.Windows;

internal record FitToEndCommand(int Threshold, int MoveTo)
{
}
