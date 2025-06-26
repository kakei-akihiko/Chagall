using System;
using System.Diagnostics;
using System.IO;

namespace Chagall.AppData;

internal class AppDataFolder
{
    public static string GetFolderPath()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;

        return Path.Combine(Environment.GetFolderPath(folder), "Chagall");
    }

    public static void RunExplorer()
    {
        var path = GetFolderPath();

        Process.Start("EXPLORER.EXE", path);

        Debug.WriteLine("Selected MainNavigationItem. Title: 設定, Path: {0}", path);
    }
}
