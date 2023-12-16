using CSharpFunctionalExtensions;
using NuGet.API.Responses;

namespace NuGet.API.Services;

public class HandleIt : IHandleIt
{
    private readonly IGetPackageVersions _getPackageVersions;
    private readonly IGetVersionInfo _getVersionInfo;

    public HandleIt(IGetPackageVersions getPackageVersions, IGetVersionInfo getVersionInfo)
    {
        _getPackageVersions = getPackageVersions;
        _getVersionInfo = getVersionInfo;
    }

    public async Task<Result<NuGetInfo, string>> Handler(string packageName, CancellationToken cancellationToken)
    {
        var x = new NuGetInfo
        {
            PackageName = packageName
        };
        
        // Get Versions
        var test = await _getPackageVersions.GetVersions(packageName, cancellationToken);

        if (test.IsFailure)
            return test.Error;
        
        // Get Nuget Info 
        foreach (var item in test.Value.Versions)
        {
            var versionAsInt = Convert.ToInt16(item[..1]);

            if (versionAsInt < 3) continue;
            
            var something = await 
                _getVersionInfo.GetVersions(packageName, item, cancellationToken);
            
            if (something.IsSuccess)
                x.VersionInfos.Add(something.Value);
        }
        
        return x;
    }
}