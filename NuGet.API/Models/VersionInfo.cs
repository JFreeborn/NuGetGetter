namespace NuGet.API.Models;

public record VersionInfo
{
    public string? Version { get; set; }

    public List<FrameworkInfo> FrameworkInfo { get; set; } = new();
}

public record FrameworkInfo
{
    public string? Product { get; set; }

    public List<string> FrameworksCompatible { get; set; } = new();
}