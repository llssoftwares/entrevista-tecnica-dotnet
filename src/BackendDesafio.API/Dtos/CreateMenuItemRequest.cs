using FluentValidation;

namespace BackendDesafio.API.Dtos;

public record CreateMenuItemRequest(string Name, string? RelatedId = null);

public class CreateMenuItemRequestRequestValidator : AbstractValidator<CreateMenuItemRequest>
{
    public CreateMenuItemRequestRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}