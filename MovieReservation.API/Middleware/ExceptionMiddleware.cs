using System.Net;
using System.Text.Json;
using MovieReservation.Business.Exceptions;

namespace MovieReservation.API.Middleware;

public class ExceptionMiddleware 
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next) 
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        (HttpStatusCode, string?) error = exception switch
        {
            ArgumentException ex => (HttpStatusCode.BadRequest, ex.Message),
            UnauthorizedAccessException ex => (HttpStatusCode.Unauthorized, ex.Message),
            InvalidOperationException ex => (HttpStatusCode.BadRequest, ex.Message),
            TimeoutException ex => (HttpStatusCode.RequestTimeout, ex.Message),
            NotImplementedException ex => (HttpStatusCode.NotImplemented, ex.Message),
            NotSupportedException ex => (HttpStatusCode.NotImplemented, ex.Message),
            OverflowException ex => (HttpStatusCode.BadRequest, ex.Message),
            FormatException ex => (HttpStatusCode.BadRequest, ex.Message),
            DuplicateFoundException ex => (HttpStatusCode.Conflict, ex.Message),
            NotFoundException ex => (HttpStatusCode.NotFound, ex.Message),
            ForbiddenException ex => (HttpStatusCode.Forbidden, ex.Message),
            UnauthorizedException ex => (HttpStatusCode.Unauthorized, ex.Message),
            _ => (HttpStatusCode.InternalServerError, exception.Message)
        };

        var httpStatusCode = error.Item1;
        var message = error.Item2;

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)httpStatusCode;

        await JsonSerializer.SerializeAsync(
            context.Response.Body,
            message);
    }
}