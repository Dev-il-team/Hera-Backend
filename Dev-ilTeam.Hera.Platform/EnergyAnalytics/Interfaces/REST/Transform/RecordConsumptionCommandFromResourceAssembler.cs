using Dev_ilTeam.Hera.Platform.EnergyAnalytics.Domain.Model.Commands;
using Dev_ilTeam.Hera.Platform.EnergyAnalytics.Interfaces.Rest.Resources;

namespace Dev_ilTeam.Hera.Platform.EnergyAnalytics.Interfaces.Rest.Transform;

public static class RecordConsumptionCommandFromResourceAssembler
{
    public static RecordConsumptionCommand ToCommandFromResource(RecordConsumptionResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(nameof(resource),
                "RecordConsumptionResource cannot be null when converting to command.");
        return new RecordConsumptionCommand(resource.DeviceName, resource.Period, resource.KilowattHours,
            resource.Cost);
    }
}