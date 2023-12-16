namespace NuGet.API.Models;

public record PackageVersions
{
    public List<string> Versions { get; } = new();
}