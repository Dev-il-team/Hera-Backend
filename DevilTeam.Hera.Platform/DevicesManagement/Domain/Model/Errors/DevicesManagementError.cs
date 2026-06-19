namespace DevilTeam.Hera.Platform.DevicesManagement.Domain.Model;

public enum DevicesManagementError
{
    None,
    DeviceNotFound,
    DuplicateDeviceName,
    InvalidDeviceData,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}