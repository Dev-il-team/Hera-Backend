namespace DevilTeam.Hera.Platform.DevicesManagement.Domain.Model.ValueObjects;

/// <summary>
///     Value object for a device's display name (US13: required, at most 50 characters).
/// </summary>
public record DeviceName
{
    public DeviceName()
    {
        Value = string.Empty;
    }

    public DeviceName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Device name is required.", nameof(value));

        value = value.Trim();

        if (value.Length > 50)
            throw new ArgumentException("Device name must be at most 50 characters.", nameof(value));

        Value = value;
    }

    public string Value { get; init; }
}
