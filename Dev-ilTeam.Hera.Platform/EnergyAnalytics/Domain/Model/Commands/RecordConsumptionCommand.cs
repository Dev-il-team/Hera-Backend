namespace Dev_ilTeam.Hera.Platform.EnergyAnalytics.Domain.Model.Commands;

public record RecordConsumptionCommand(string DeviceName, string Period, decimal KilowattHours, decimal Cost);