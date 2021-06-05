using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

public class AppInsightsFilter : ITelemetryProcessor
{
    private ITelemetryProcessor Next { get; set; }
    private Dictionary<string, string> ExcludedFormats { get; set; }

    public AppInsightsFilter(ITelemetryProcessor next)
    {
        this.Init();
        this.Next = next;
    }

    public void Process(ITelemetry item)
    {
        if (FilterOutNotFound(item)) 
        { 
            return; 
        }

        if (FilterOutFileExtensions(item)) 
        { 
            return; 
        }

        this.Next.Process(item);
    }

    private void Init()
    {
        ExcludedFormats = new Dictionary<string, string>();
        ExcludedFormats.Add(".map", ".map");
        ExcludedFormats.Add(".css", ".css");
        ExcludedFormats.Add(".js", ".js");
        ExcludedFormats.Add(".txt", ".txt");
        ExcludedFormats.Add(".png", ".png");
        ExcludedFormats.Add(".ico", ".ico");
        ExcludedFormats.Add(".xml", ".xml");
        ExcludedFormats.Add(".php", ".php");
        ExcludedFormats.Add(".env", ".env");
        ExcludedFormats.Add(".jpg", ".jpg");
    }
    private bool FilterOutNotFound(ITelemetry item)
    {
        var request = item as RequestTelemetry;
        var notFoundCode = (int)HttpStatusCode.NotFound;

        if (request != null && request.ResponseCode.Equals(notFoundCode.ToString(), StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }        

        return false;
    }

    private bool FilterOutFileExtensions(ITelemetry item)
    {
        var request = item as RequestTelemetry;

        if (request != null)
        {
            var ext = Path.GetExtension(request.Url.ToString());

            Console.WriteLine(ext);

            if (ext != string.Empty && ExcludedFormats.ContainsKey(ext))
            {
                return true;                
            }
        }        

        return false;
    }
}