using Microsoft.AspNetCore.Mvc;
using NuGet.API.Requests;
using NuGet.API.Responses;
using NuGet.API.Services;

namespace NuGet.API.Controllers;

[ApiController]
[Route("[controller]")]
public class GetInfoController : ControllerBase
{
    private readonly ILogger<GetInfoController> _logger;
    private readonly IHandleIt _handleIt;

    public GetInfoController(ILogger<GetInfoController> logger, IHandleIt handleIt)
    {
        _logger = logger;
        _handleIt = handleIt;
    }

    // TODO need to make a envelope that has the error or value in it
    [HttpGet(Name = "GetWeatherForecast2")]
    public async Task<NuGetInfo> Get([FromQuery] GetInfoRequest request)
    {
        _logger.LogInformation("here is a log message");

        var z = await _handleIt.Handler(request.NuGetPackageName, CancellationToken.None);

        if (z.IsFailure)
            throw new Exception(z.Error);
        
        return z.Value;
    }
}