using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using WinRT.Interop;

namespace Chagall.ExMethods
{
    public static class WindowEx
    {
        public static AppWindow GetAppWindow(this Window window)
        {
            var handle = WindowNative.GetWindowHandle(window);

            var windowId = Win32Interop.GetWindowIdFromWindow(handle);

            return AppWindow.GetFromWindowId(windowId);
        }

        // 最大化しているかどうかを取得
        public static bool IsMaximized(this Window window)
        {
            var appWindow = window.GetAppWindow();

            return appWindow.IsMaximized();
        }

        // 最大化しているかどうかを取得
        public static bool IsMaximized(this AppWindow appWindow)
        {
            var overlappedPresenter = appWindow.Presenter as OverlappedPresenter;

            return overlappedPresenter?.State == OverlappedPresenterState.Maximized;
        }

    }
}
