using DevilTeam.Hera.Platform.DevicesManagement.Domain.Model.Commands;
using DevilTeam.Hera.Platform.DevicesManagement.Domain.Model.ValueObjects;
using DevilTeam.Hera.Platform.Shared.Domain.Model.Entities;

namespace DevilTeam.Hera.Platform.DevicesManagement.Domain.Model.Aggregates;

/// <summary>
///     Device aggregate root: a single piece of smart-home hardware linked to one account.
///     Power state (<see cref="IsOn" />) and reachability (<see cref="Connectivity" />) are
///     two independent facts and must not be collapsed.
/// </summary>
public class Device : IAuditableEntity
{
    public Device()
    {
        Name = new DeviceName();
        Code = new DeviceCode();
        Room = string.Empty;
    }

    public Device(CreateDeviceCommand command)
    {
        Name = new DeviceName(command.Name);
        Type = ParseType(command.Type);
        Code = new DeviceCode(command.Code);
        Room = command.Room?.Trim() ?? string.Empty;
        OwnerId = command.OwnerId;
        IsOn = false;
        Connectivity = ConnectivityStatus.Offline;
    }

    public int Id { get; }
    public DeviceName Name { get; private set; }
    public DeviceType Type { get; private set; }
    public string Room { get; private set; }
    public bool IsOn { get; private set; }
    public ConnectivityStatus Connectivity { get; private set; }
    public DeviceCode Code { get; private set; }

    /// <summary>
    ///     The account this device belongs to, referenced by id only (no FK/navigation).
    ///     Ownership enforcement is deferred to IAM and currently unenforced.
    /// </summary>
    public int? OwnerId { get; private set; }

    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    /// <summary>Convenience accessors for assemblers.</summary>
    public string DisplayName => Name.Value;

    public string DeviceCodeValue => Code.Value;

    /// <summary>Rename the device (US13).</summary>
    public void Rename(string name)
    {
        Name = new DeviceName(name);
    }

    /// <summary>Assign the device to a room/zone (US15).</summary>
    public void AssignToRoom(string room)
    {
        Room = room?.Trim() ?? string.Empty;
    }

    /// <summary>Turn the device on or off (US17 / US38).</summary>
    public void SetPowerState(bool isOn)
    {
        IsOn = isOn;
    }

    private static DeviceType ParseType(string type)
    {
        if (string.IsNullOrWhiteSpace(type) || !Enum.TryParse<DeviceType>(type, true, out var parsed))
            throw new ArgumentException($"Unknown device type '{type}'.", nameof(type));
        return parsed;
    }
}
