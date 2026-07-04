using Dev_ilTeam.Hera.Platform.Monitoring.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.Monitoring.Interfaces.Rest.Resources;

namespace Dev_ilTeam.Hera.Platform.Monitoring.Interfaces.Rest.Transform;

/// <summary>
/// Assembler responsible for transforming a <see cref="Camera" /> into an
/// <see cref="CameraResource" />.
/// </summary>
public static class CameraResourceFromEntityAssembler
{
    /// <summary>
    /// Converts a <see cref="Camera" /> to a <see cref="CameraResource" />.
    /// </summary>
    /// <param name="entity">
    /// The <see cref="Camera" /> containing the camera data. Must not be null.
    /// </param>
    /// <returns>
    /// A new <see cref="CameraResource" /> instance.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown if the input <paramref name="entity" /> is null.</exception>
    public static CameraResource ToResourceFromEntity(Camera entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity),
                "Camera entity cannot be null when converting to resource.");
        return new CameraResource(entity.Id, entity.Name, entity.Location, entity.StreamUrl, entity.Status.ToString());
    }
}
