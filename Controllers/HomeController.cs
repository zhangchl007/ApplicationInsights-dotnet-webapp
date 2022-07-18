using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using videowebapp.Models;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;

namespace videowebapp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly TelemetryClient _telemetryClient;
    private readonly TelemetryConfiguration _telemetryConfiguration;

    
    public HomeController(ILogger<HomeController> logger,TelemetryClient telemetryClient,TelemetryConfiguration telemetryConfiguration)
    {
        _logger = logger;
        _telemetryClient = telemetryClient;
        _telemetryConfiguration = telemetryConfiguration;

    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult Like(string button)
    {
        ViewBag.Message = "Thank you for your response";
        _telemetryClient.TrackEvent(eventName: "LikeClicked");
        _telemetryClient.TrackEvent(eventName: "CommentSubmitted");
        _telemetryClient.TrackEvent("VideoUploaded", new Dictionary<string, string> {{"Category", "Sports"}, {"Format", "mp4"}});
        Metric userResponse = _telemetryClient.GetMetric("UserResponses", "Kind");

        userResponse.TrackValue(24, "Likes");
        userResponse.TrackValue(5, "Loves");
        return View("Index");
       
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
