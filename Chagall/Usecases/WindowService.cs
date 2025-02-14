using Chagall.Infrastructure.Processes;
using Chagall.Domain.Features.Windows;
using Chagall.Infrastructure.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using Window = Chagall.Domain.Features.Windows.Window;

namespace Chagall.Usecases
{
    class WindowService
    {
        public IEnumerable<Window> GetWindows()
        {
            foreach (var handle in windowRepository.EnumWindowHandles())
            {
                var rectangle = windowRepository.GetRectangle(handle);

                var text = windowRepository.GetText(handle);

                var visible = windowRepository.IsVisible(handle);

                if (visible && string.IsNullOrEmpty(text) == false)
                    DebugWrite(text, rectangle);

                if (IsTarget(rectangle.Width, rectangle.Height, visible, text) == false)
                    continue;
                Debug.WriteLine("★WindowText: {0}", text);

                var processId = windowRepository.GetProcessId(handle);

                var path = processRepository.GetMainModuleProcessPath(processId);
                Debug.WriteLine("★{0} {1} {2}", text, processId, path);

                var window = new Window();
                window.Handle = handle;
                window.MainModulePath = path;
                window.ProcessId = processId;
                window.Rectangle = rectangle;
                window.Text = text;
                window.Visible = visible;
                yield return window;
            }
        }

        public void Run(List<WindowPolicy> policies, WindowCommand otherWindowCommand)
        {
            var desktopHandle = windowRepository.GetDesktopHandle();

            var desktopRectangle = windowRepository.GetRectangle(desktopHandle);

            foreach (var window in GetWindows())
            {
                var policy = policies.Find(p => window.Text != null && IsMatch(window.Text, window.Handle, p.Filter));
                if (policy == null)
                    Apply(window.Handle, window.Rectangle, otherWindowCommand);
                else Apply(window.Handle, window.Rectangle, policy.Command);
            }

            DebugWrite("デスクトップ", desktopRectangle);
        }

        readonly ProcessRepository processRepository = new();
        readonly WindowRepository windowRepository = new();

        private void DebugWrite(string text, Int32Rect rect)
        {
            Debug.WriteLine("{0} X: {1}, Y: {2}, W: {3}, H: {4}",
                text, rect.X, rect.Y, rect.Width, rect.Height);
        }

        private bool IsTarget(int width, int height, bool isVisible, string text) =>
            width > 0 && height > 0 && isVisible && string.IsNullOrEmpty(text) == false
            && text != "SplashtopBlankScreen";

        private bool IsMatch(string text, IntPtr handle, WindowFilter filter)
        {
            if (filter.TitleEndsWith != null)
            {
                if (text == null || text.EndsWith(filter.TitleEndsWith) == false)
                    return false;
            }

            if (filter.ProcessFileName != null)
            {
                var processId = windowRepository.GetProcessId(handle);

                var path = processRepository.GetMainModuleProcessPath(processId);

                if (path == null || Regex.IsMatch(path, filter.ProcessFileName, RegexOptions.IgnoreCase) == false)
                    return false;
            }

            return true;
        }

        private void Apply(IntPtr handle, Int32Rect rectangle, WindowCommand command)
        {
            var moveTo = rectangle;

            if (command.FitXToGrid != null)
                moveTo = FitXToGrid(moveTo, command.FitXToGrid);
            if (command.FitYToGrid != null)
                moveTo = FitYToGrid(moveTo, command.FitYToGrid);
            if (command.FitBottom != null)
                moveTo = FitBottom(moveTo, command.FitBottom);
            if (command.FitRight != null)
                moveTo = FitRight(moveTo, command.FitRight);
            if (command.FitBottomRight != null)
                moveTo = FitBottomRight(moveTo, command.FitBottomRight);

            windowRepository.SetRectangle(handle, moveTo);
        }

        private Int32Rect FitXToGrid(Int32Rect rectangle, FitToGridCommand fitToGrid)
        {
            var offset = fitToGrid.Offset;
            var gridSize = fitToGrid.GridSize;
            var x = (rectangle.X + offset + gridSize / 2) / gridSize * gridSize + offset;

            return new Int32Rect(
                x,
                rectangle.Y,
                rectangle.Width,
                rectangle.Height
            );
        }

        private Int32Rect FitYToGrid(Int32Rect rectangle, FitToGridCommand fitToGrid)
        {
            var offset = fitToGrid.Offset;
            var gridSize = fitToGrid.GridSize;
            var y = (rectangle.Y + offset + gridSize / 2) / gridSize * gridSize + offset;

            return new Int32Rect(
                rectangle.X,
                y,
                rectangle.Width,
                rectangle.Height
            );
        }

        private Int32Rect FitBottomRight(Int32Rect rectangle, FitBottomRightCommand fit)
        {
            return new Int32Rect(
                rectangle.X,
                rectangle.Y,
                fit.Right - rectangle.X,
                fit.Bottom - rectangle.Y
            );
        }

        // ウインドウの右端がthreshold以上であればウインドウ幅を調整して右端をmoveToに合わせる。
        private Int32Rect FitRight(Int32Rect rectangle, FitToEndCommand fit)
        {
            var width = rectangle.X + rectangle.Width >= fit.Threshold ? fit.MoveTo - rectangle.X : rectangle.Width;

            return new Int32Rect(
                rectangle.X,
                rectangle.Y,
                width,
                rectangle.Height
            );
        }

        // ウインドウの下端がthreshold以上であればウインドウの高さを調整して下端をmoveToに合わせる。
        private Int32Rect FitBottom(Int32Rect rectangle, FitToEndCommand fit)
        {
            var height = rectangle.Y + rectangle.Height >= fit.Threshold ? fit.MoveTo - rectangle.Y : rectangle.Height;

            return new Int32Rect(
                rectangle.X,
                rectangle.Y,
                rectangle.Width,
                height
            );
        }
    }
}
