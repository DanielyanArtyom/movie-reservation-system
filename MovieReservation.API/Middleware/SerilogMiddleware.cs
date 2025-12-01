using System.Diagnostics;
using Serilog;
using Serilog.Events;

namespace MovieReservation.API.Middleware;

public class SerilogMiddleware
{
     const string MessageTemplate =
        "HTTP {RequestMethod} {RequestPath} from {ClientIP} responded {StatusCode} in {Elapsed:0.0000} ms. Request: {RequestContent}, Response: {ResponseContent}";

    readonly RequestDelegate _next;

    public SerilogMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public async Task Invoke(HttpContext httpContext)
    {
        if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));

        var sw = Stopwatch.StartNew();

        httpContext.Request.EnableBuffering();
        var requestBody = await ReadRequestBodyAsync(httpContext.Request);
        
        httpContext.Request.Body.Position = 0;
        var originalResponseBodyStream = httpContext.Response.Body;
        
        await using var responseBodyStream = new MemoryStream();
        httpContext.Response.Body = responseBodyStream;

        try
        {
            await _next(httpContext);
            sw.Stop();

            var statusCode = httpContext.Response?.StatusCode;
            var level = statusCode > 499 ? LogEventLevel.Error : LogEventLevel.Information;

            httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(httpContext.Response.Body).ReadToEndAsync();
            
            httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
            await responseBodyStream.CopyToAsync(originalResponseBodyStream);

            var clientIP = httpContext.Connection.RemoteIpAddress.ToString();

            Log.Write(level, MessageTemplate, httpContext.Request.Method, httpContext.Request.Path, clientIP, statusCode, sw.Elapsed.TotalMilliseconds, requestBody, responseBody);
        }
        catch (Exception exception)
        {
              Log.Error(exception, "HTTP Error:  {RequestMethod} {RequestPath} responded {StatusCode} in {ElapsedMilliseconds}ms\nIP: {RemoteIpAddress}\nException Type: {ExceptionType}\nException Message: {ExceptionMessage}\nInner Exceptions: {InnerExceptions}\nException Details: {ExceptionDetails}\nStack Trace: {StackTrace}",
                  httpContext.Request.Method,
                  httpContext.Request.Path,
                  httpContext.Response.StatusCode,
                  sw.ElapsedMilliseconds,
                  httpContext.Connection.RemoteIpAddress?.ToString(),
                  exception.GetType().Name,
                  exception.Message,
                  exception.InnerException?.Message,
                  exception.ToString(),
                  exception.StackTrace);
              throw;
        }
        finally
        {
            httpContext.Response.Body = originalResponseBodyStream;
        }
    }

   
    static async Task<string> ReadRequestBodyAsync(HttpRequest request)
    {
        request.Body.Position = 0;
        using (var reader = new StreamReader(request.Body, leaveOpen: true))
        {
            var body = await reader.ReadToEndAsync();
            request.Body.Position = 0;
            return body;
        }
    }
}