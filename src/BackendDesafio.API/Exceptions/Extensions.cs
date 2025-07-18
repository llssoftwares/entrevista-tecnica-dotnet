namespace BackendDesafio.API.Exceptions;

public static class Extensions
{
    public static IServiceCollection AddExceptionHandling(this IServiceCollection services)
    {
        return services
            .AddExceptionHandler<GlobalExceptionHandler>()
            .AddProblemDetails();
    }
}