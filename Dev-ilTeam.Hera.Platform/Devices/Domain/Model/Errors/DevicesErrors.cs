using Dev_ilTeam.Hera.Platform.Shared.Domain.Model;

namespace Dev_ilTeam.Hera.Platform.Devices.Domain.Model.Errors;

public static class DevicesErrors
{
    public static readonly Error RoomNotFound =
        new("Devices.RoomNotFound", "The specified room was not found.");

    public static readonly Error DeviceNotFound =
        new("Devices.DeviceNotFound", "The specified device was not found.");

    public static readonly Error DuplicateDeviceName =
        new("Devices.DuplicateDeviceName", "A device with the specified name already exists.");
}
