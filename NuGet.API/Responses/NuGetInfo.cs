using NuGet.API.Models;

namespace NuGet.API.Responses;

public record NuGetInfo
{
    public string? PackageName { get; set; }
    public List<VersionInfo> VersionInfos { get; set; } = new();
}