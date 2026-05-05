namespace HotelsCalifornia.Middleware;
using System.Text.Json;

public class GlobalExceptionHandler(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            await HandleException(context, e);
        }
    }

    private async Task HandleException(HttpContext context, Exception e)
    {
        var statusCode = e switch
        {
            KeyNotFoundException _ or NullReferenceException _ => 404,
            ArgumentOutOfRangeException _ or ArgumentException _ => 400,
            UnauthorizedAccessException _ => 401,
            _ => 500,
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        var body = JsonSerializer.Serialize(new
        {
            status = statusCode,
            message = e.Message
        });
        await context.Response.WriteAsync(body);
    }
}