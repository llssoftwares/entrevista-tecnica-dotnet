using BackendDesafio.API.Domain;
using System.Text.Json;

namespace BackendDesafio.API.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (DomainException dex)
        {
            await HandleExceptionAsync(context, 400, dex.Message);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, int statusCode, string message)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        var problem = new
        {
            status = statusCode,
            title = message
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(problem));
    }
}