namespace Dev_ilTeam.Hera.Platform.EnergyAnalytics.Domain.Model.ValueObjects;

public record EnergyMeasurement(decimal KilowattHours, decimal Cost)
{
    public EnergyMeasurement() : this(0m, 0m)
    {
    }
}