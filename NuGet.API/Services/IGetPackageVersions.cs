using CSharpFunctionalExtensions;
using NuGet.API.Models;

namespace NuGet.API.Services;

public interface IGetPackageVersions
{
    public Task<Result<PackageVersions, string>> GetVersions(string packageId, CancellationToken cancellationToken);
}