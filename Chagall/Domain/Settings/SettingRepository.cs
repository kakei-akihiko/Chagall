using Chagall.Domain.Features.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Chagall.Domain.Settings;

internal class SettingRepository
{
    public void Save(AppSetting setting)
    {
        var filename = GetFilename();

        var directoryName = Path.GetDirectoryName(filename);

        if (string.IsNullOrEmpty(directoryName))
            throw new ApplicationException();

        if (Directory.Exists(directoryName) == false)
            Directory.CreateDirectory(directoryName);

        var json = JsonSerializer.Serialize(setting);

        File.WriteAllText(filename, json, Encoding.UTF8);
    }

    public AppSetting Load()
    {
        var setting = LoadInternal();

        if (setting != null)
            return setting;

        setting = GetDefaultSetting();

        Save(setting);

        return setting;
    }

    public string GetFolderPath()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;

        return Path.Combine(Environment.GetFolderPath(folder), "Chagall");
    }

    private AppSetting? LoadInternal()
    {
        var filename = GetFilename();

        try
        {
            var json = File.ReadAllText(filename, Encoding.UTF8);

            return JsonSerializer.Deserialize<AppSetting>(json);
        }
        catch (IOException)
        {
            return null;
        }
    }

    private string GetFilename()
    {
        return Path.Combine(GetFolderPath(), "Settings.json");
    }

    private AppSetting GetDefaultSetting()
    {
        var policies = new List<WindowPolicy>()
            {
                CreateVSCodePolicy(),
                CreateSourceTreePolicy()
            };

        var otherWindowCommand = CreateOtherWindowCommand();

        return new AppSetting(policies, otherWindowCommand);
    }

    private WindowPolicy CreateVSCodePolicy()
    {
        var filter = new WindowFilter(" - Visual Studio Code", "Code.exe");

        var fitXToGrid = new FitToGridCommand(50, 0);
        var fitYToGrid = new FitToGridCommand(25, 0);
        var fitBottomRight = new FitBottomRightCommand(1039, 1914);
        var command = new WindowCommand(fitXToGrid, fitYToGrid, null, null, fitBottomRight);

        return new WindowPolicy(filter, command);
    }

    private WindowPolicy CreateSourceTreePolicy()
    {
        var filter = new WindowFilter("Sourcetree", "SourceTree.exe");

        var fitXToGrid = new FitToGridCommand(50, 7);
        var fitYToGrid = new FitToGridCommand(25, 1);
        var fitBottomRight = new FitBottomRightCommand(1042, 1909);
        var command = new WindowCommand(fitXToGrid, fitYToGrid, null, null, fitBottomRight);

        return new WindowPolicy(filter, command);
    }

    private WindowCommand CreateOtherWindowCommand()
    {
        var fitXToGrid = new FitToGridCommand(50, 0);
        var fitYToGrid = new FitToGridCommand(25, 0);
        var bottom = new FitToEndCommand(1030, 1044);
        var right = new FitToEndCommand(1900, 1919);
        return new WindowCommand(fitXToGrid, fitYToGrid, bottom, right, null);
    }
}
