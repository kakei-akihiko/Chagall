namespace Chagall.Infrastructure.Processes;

class ProcessRepository
{
    public string? GetMainModuleProcessPath(int processId)
    {
        return System.Diagnostics.Process
            .GetProcessById(processId)?.MainModule?.FileName;
    }
}
