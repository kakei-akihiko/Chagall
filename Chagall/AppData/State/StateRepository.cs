using Chagall.Domain.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Chagall.AppData.State;

internal class StateRepository
{
    public void Save(AppState state)
    {
        var filename = GetFilename();

        var directoryName = Path.GetDirectoryName(filename);

        if (string.IsNullOrEmpty(directoryName))
            throw new ApplicationException();

        if (Directory.Exists(directoryName) == false)
            Directory.CreateDirectory(directoryName);

        var json = JsonSerializer.Serialize(state);

        File.WriteAllText(filename, json, Encoding.UTF8);
    }

    public AppState Load()
    {
        var filename = GetFilename();

        try
        {
            var json = File.ReadAllText(filename, Encoding.UTF8);

            return JsonSerializer.Deserialize<AppState>(json) ?? GetDefault();
        }
        catch (IOException ex)
        {
            Debug.WriteLine("[WARN] 状態ファイルの読み込みに失敗しました。\n{0}", ex);
            return GetDefault();
        }
    }

    private string GetFilename()
    {
        return Path.Combine(AppDataFolder.GetFolderPath(), "State.json");
    }

    private AppState GetDefault() => new(new(null));

}
