namespace BackendDesafio.API.Middlewares;

public static class Extensions
{
    public static IApplicationBuilder UseMiddlewares(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}