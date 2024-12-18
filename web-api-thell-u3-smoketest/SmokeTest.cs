using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace web_api_thell_u3_smoketest;

public class SmokeTest
{
    private readonly ILogger<SmokeTest> _logger;
    private readonly HttpClient _httpClient;

    public SmokeTest(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<SmokeTest>();
        _httpClient = new HttpClient();
    }

    [Function("SmokeTest")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
    {
        var apiResponse = await _httpClient.GetAsync("https://web-api-thell-u3-d8e6ehfgesf5efdq.westeurope-01.azurewebsites.net/WeatherForecast");
        var response = req.CreateResponse(apiResponse.IsSuccessStatusCode ? HttpStatusCode.OK : HttpStatusCode.ServiceUnavailable);
        return response;
    }
}