namespace Dev_ilTeam.Hera.Platform.EnergyAnalytics.Interfaces.Rest.Resources;

public record ConsumptionReportResource(int Id, string DeviceName, string Period, decimal KilowattHours, decimal Cost);