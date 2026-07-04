using Dev_ilTeam.Hera.Platform.Devices.Domain.Model.Commands;
using Dev_ilTeam.Hera.Platform.Devices.Domain.Model.Entities;
using Dev_ilTeam.Hera.Platform.Devices.Domain.Model.ValueObjects;

namespace Dev_ilTeam.Hera.Platform.Devices.Domain.Model.Aggregates;

public partial class Device
{
    public Device()
    {
        Name = string.Empty;
        Type = EDeviceType.Light;
        Status = EDeviceStatus.Off;
    }

    public Device(string name, EDeviceType type, int roomId) : this()
    {
        Name = name;
        Type = type;
        RoomId = roomId;
    }

    public Device(RegisterDeviceCommand command) : this(command.Name, command.Type, command.RoomId)
    {
    }

    public int Id { get; }
    public string Name { get; private set; }
    public EDeviceType Type { get; private set; }
    public EDeviceStatus Status { get; private set; }

    public Room Room { get; internal set; }

    public int RoomId { get; private set; }

    public Device Rename(string name)
    {
        Name = name;
        return this;
    }

    public void TurnOn()
    {
        Status = EDeviceStatus.On;
    }

    public void TurnOff()
    {
        Status = EDeviceStatus.Off;
    }

    public void MarkAsDisconnected()
    {
        Status = EDeviceStatus.Disconnected;
    }

    public Device AssignToRoom(int roomId)
    {
        RoomId = roomId;
        return this;
    }
}
