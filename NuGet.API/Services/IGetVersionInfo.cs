using CSharpFunctionalExtensions;
using NuGet.API.Models;

namespace NuGet.API.Services;

public interface IGetVersionInfo
{
    public Task<Result<VersionInfo, string>> GetVersions(string packageId, string version, CancellationToken cancellationToken);
}