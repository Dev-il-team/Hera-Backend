using Dev_ilTeam.Hera.Platform.Devices.Domain.Model.ValueObjects;

namespace Dev_ilTeam.Hera.Platform.Devices.Interfaces.Rest.Resources;

public record RegisterDeviceResource(string Name, EDeviceType Type, int RoomId);
