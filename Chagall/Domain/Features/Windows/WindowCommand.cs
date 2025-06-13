namespace Chagall.Domain.Features.Windows;

internal record WindowCommand(
    FitToGridCommand? FitXToGrid,
    FitToGridCommand? FitYToGrid,
    FitToEndCommand? FitBottom,
    FitToEndCommand? FitRight,
    FitBottomRightCommand? FitBottomRight)
{
}
