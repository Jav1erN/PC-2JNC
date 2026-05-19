namespace PC_2JNC.Domain.ValueObjects;

public sealed record Email
{
    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("El email no puede estar vacío.", nameof(value));
        }

        if (!value.Contains('@', StringComparison.Ordinal))
        {
            throw new ArgumentException("El email no tiene un formato válido.", nameof(value));
        }

        Value = value.Trim();
    }

    public string Value { get; }

    public override string ToString()
    {
        return Value;
    }
}
