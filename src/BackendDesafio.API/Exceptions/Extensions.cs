namespace BackendDesafio.API.Exceptions;

public static class Extensions
{
    public static IServiceCollection AddExceptionHandling(this IServiceCollection services)
    {
        return services
            .AddExceptionHandler<GlobalExceptionHandler>()
            .AddProblemDetails(config =>
            {
                config.CustomizeProblemDetails = context =>
                {
                    context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);
                };
            });
    }
}