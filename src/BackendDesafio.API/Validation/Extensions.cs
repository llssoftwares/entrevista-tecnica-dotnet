using BackendDesafio.API.Dtos;
using FluentValidation;

namespace BackendDesafio.API.Validation;

public static class Extensions
{
    public static IServiceCollection AddValidation(this IServiceCollection services)
    {
        return services.AddValidatorsFromAssemblyContaining<CreateMenuItemRequestRequestValidator>();
    }

    public static RouteHandlerBuilder WithValidation<T>(this RouteHandlerBuilder builder)
    {
        return builder.AddEndpointFilter<ValidationFilter<T>>();
    }
}