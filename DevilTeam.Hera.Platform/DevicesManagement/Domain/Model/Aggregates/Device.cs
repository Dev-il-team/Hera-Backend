using DevilTeam.Hera.Platform.DevicesManagement.Domain.Model.Commands;

namespace DevilTeam.Hera.Platform.DevicesManagement.Domain.Model.Aggregates;

/// <summary>
///     Device aggregate root.
/// </summary>
public partial class Device
{
    public Device()
    {
        Name = string.Empty;
        Type = string.Empty;
        Room = string.Empty;
        Status = string.Empty;
    }

    public Device(CreateDeviceCommand command)
    {
        Name = command.Name;
        Type = command.Type;
        Room = command.Room;
        Status = command.Status;
        EnergyConsumption = command.EnergyConsumption;
    }

    public int Id { get; }
    public string Name { get; private set; }
    public string Type { get; private set; }
    public string Room { get; private set; }
    public string Status { get; private set; }
    public decimal EnergyConsumption { get; private set; }
}