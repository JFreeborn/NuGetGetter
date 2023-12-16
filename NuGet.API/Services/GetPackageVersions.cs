using System.Net;
using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using CSharpFunctionalExtensions.Json.Serialization;
using NuGet.API.Models;

namespace NuGet.API.Services;

public class GetPackageVersions : IGetPackageVersions
{
    public async Task<Result<PackageVersions, string>> GetVersions(string packageId, CancellationToken cancellationToken)
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri("https://api.nuget.org/v3-flatcontainer/");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
    
        var requestUrl = $"{packageId.ToLower()}/index.json";
        var response = await client.GetAsync(requestUrl, cancellationToken);

        var result = new PackageVersions();
        
        if (response.StatusCode == HttpStatusCode.NotFound)
            return $"{packageId} was not found";
        
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
    
        const string pattern = @"\s+\""(\d{1,}\.\d{1,}\.\d{1,})\""";

        var regEx = new Regex(pattern);

        var matches = regEx.Matches(responseBody);
    
        foreach (Match match in matches)
        {
            var packageVersion = match.Groups[1].Value;
        
            result.Versions.Add(packageVersion);            
        }

        return result;
    }
}