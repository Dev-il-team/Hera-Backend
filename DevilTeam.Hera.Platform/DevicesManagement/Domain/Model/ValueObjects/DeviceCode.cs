namespace DevilTeam.Hera.Platform.DevicesManagement.Domain.Model.ValueObjects;

/// <summary>
///     Value object for the pairing code printed on the hardware, supplied when linking a
///     device (US11). Identifies the physical unit and is validated for shape at link time.
/// </summary>
public record DeviceCode
{
    public DeviceCode()
    {
        Value = string.Empty;
    }

    public DeviceCode(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Device code is required.", nameof(value));

        Value = value.Trim();
    }

    public string Value { get; init; }
}
