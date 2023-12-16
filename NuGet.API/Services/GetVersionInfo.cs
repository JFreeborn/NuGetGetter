using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using NuGet.API.Models;

namespace NuGet.API.Services;

public class GetVersionInfo : IGetVersionInfo
{
    public async Task<Result<VersionInfo, string>> GetVersions(string packageName, string packageVersion, CancellationToken cancellationToken)
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri("https://www.nuget.org/packages/");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        var requestUrl = $"{packageName.ToLower()}/{packageVersion.ToLower()}";
        var response = await client.GetAsync(requestUrl, cancellationToken);

        if (!response.IsSuccessStatusCode)
            return "internal server error";

        var versionInfoResult = new VersionInfo
        {
            Version = packageVersion
        };

        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);

        const string pattern = @"<tr>\s*<td[^>]*>\s*(.*\.NET.*?)\s*<\/td>\s*<td[^>]*>\s*((?:<span[^>]*>.*?<\/span>\s*)*)<\/td>\s*<\/tr>";
        const string pattern2 = @"(\<span\sclass\=\""sr\-only\""\>)((.*)\sis\scompatible.*)(\.\&)";
        //const string pattern3 = @"(\<span\sclass\=\""sr\-only\""\>)((.*)\swas\scomputed.*)(\.\&)";
        
        var regex = new Regex(pattern);
        var regex2 = new Regex(pattern2);
        //var regex3 = new Regex(pattern3);
        
        var matches = regex.Matches(responseBody);
        
        foreach (Match match in matches)
        {
            var z = new FrameworkInfo
            {
                Product = match.Groups[1].Value.Trim()
            };

            var matches2 = regex2.Matches(match.Groups[2].Value);
            //var matches3 = regex3.Matches(match.Groups[2].Value);
            
            //Console.WriteLine("Frameworks Compatible:");
            
            foreach (Match match2 in matches2)
            {
                //Console.WriteLine(match2.Groups[3].Value);
                
                z.FrameworksCompatible.Add(match2.Groups[3].Value);
                
               //versionInfoResult.FrameworkInfo.Add(match2.Groups[3].Value);
            }
            
            versionInfoResult.FrameworkInfo.Add(z);

            //Console.WriteLine("");
            //Console.WriteLine("Frameworks Computed:");
            
            // foreach (Match match3 in matches3)
            // {
            //     Console.WriteLine(match3.Groups[3].Value);
            // }
            
            Console.WriteLine();
        }

        return versionInfoResult;
    }
}