namespace Dev_ilTeam.Hera.Platform.EnergyAnalytics.Interfaces.Rest.Resources;

public record RecordConsumptionResource(string DeviceName, string Period, decimal KilowattHours, decimal Cost);