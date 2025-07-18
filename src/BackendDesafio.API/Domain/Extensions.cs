namespace BackendDesafio.API.Domain;

public static class Extensions
{
    public static int? ToNullableInt(this string? value)
    {
        return string.IsNullOrEmpty(value)
            ? null
            : int.TryParse(value, out var result)
                ? result
                : null;
    }
}
