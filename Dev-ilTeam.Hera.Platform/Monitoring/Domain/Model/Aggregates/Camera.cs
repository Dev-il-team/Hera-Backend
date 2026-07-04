using Dev_ilTeam.Hera.Platform.Monitoring.Domain.Model.Commands;
using Dev_ilTeam.Hera.Platform.Monitoring.Domain.Model.ValueObjects;

namespace Dev_ilTeam.Hera.Platform.Monitoring.Domain.Model.Aggregates;

public partial class Camera
{
    public Camera()
    {
        Name = string.Empty;
        Location = string.Empty;
        StreamUrl = string.Empty;
        Status = ECameraStatus.Idle;
    }

    public Camera(string name, string location, string streamUrl) : this()
    {
        Name = name;
        Location = location;
        StreamUrl = streamUrl;
    }

    public Camera(RegisterCameraCommand command) : this(command.Name, command.Location, command.StreamUrl)
    {
    }

    public int Id { get; }
    public string Name { get; private set; }
    public string Location { get; private set; }
    public string StreamUrl { get; private set; }
    public ECameraStatus Status { get; private set; }

    public void StartStreaming()
    {
        Status = ECameraStatus.Streaming;
    }

    public void MarkAsOffline()
    {
        Status = ECameraStatus.Offline;
    }
}
