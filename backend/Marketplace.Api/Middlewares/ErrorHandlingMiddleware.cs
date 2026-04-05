using System.Net;
using System.Text.Json;
using Marketplace.Application.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await _HandleException(context, ex);
        }
    }

    private static Task _HandleException(HttpContext context, Exception ex)
    {
        var response = context.Response;
        response.ContentType = "application/json";
        response.StatusCode = ex switch
        {
            BadRequestException => (int)HttpStatusCode.BadRequest,
            UnauthorizedException => (int)HttpStatusCode.Unauthorized,
            _ => (int)HttpStatusCode.InternalServerError,
        };

        var result = JsonSerializer.Serialize(new
        {
            error = ex.Message,
            stackTrace = ex.StackTrace
        });
        return response.WriteAsync(result);
    }
}