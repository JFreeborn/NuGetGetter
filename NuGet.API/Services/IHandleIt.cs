using CSharpFunctionalExtensions;
using NuGet.API.Responses;

namespace NuGet.API.Services;

public interface IHandleIt
{
    public Task<Result<NuGetInfo,string>> Handler(string packageName, CancellationToken cancellationToken);
}