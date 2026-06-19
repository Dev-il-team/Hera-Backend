namespace DevilTeam.Hera.Platform.DevicesManagement.Domain.Model;

/// <summary>
///     Error codes for the Devices Management context, mapped to HTTP status codes by
///     the REST action-result assembler and to localized messages by the Result pipeline.
/// </summary>
public enum DevicesError
{
    None,
    DeviceNotFound,
    InvalidDeviceData,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
