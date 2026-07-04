using Dev_ilTeam.Hera.Platform.Devices.Domain.Model.ValueObjects;

namespace Dev_ilTeam.Hera.Platform.Devices.Domain.Model.Commands;

public record RegisterDeviceCommand(string Name, EDeviceType Type, int RoomId);
