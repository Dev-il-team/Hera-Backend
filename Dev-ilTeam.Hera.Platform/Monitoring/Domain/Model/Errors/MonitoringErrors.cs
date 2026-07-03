using Dev_ilTeam.Hera.Platform.Shared.Domain.Model;

namespace Dev_ilTeam.Hera.Platform.Monitoring.Domain.Model.Errors;

public static class MonitoringErrors
{
    public static readonly Error CameraNotFound =
        new("Monitoring.CameraNotFound", "The specified camera was not found.");

    public static readonly Error DuplicateCameraName =
        new("Monitoring.DuplicateCameraName", "A camera with the specified name already exists.");
}
