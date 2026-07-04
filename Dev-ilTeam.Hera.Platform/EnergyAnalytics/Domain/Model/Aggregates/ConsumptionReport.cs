using Dev_ilTeam.Hera.Platform.EnergyAnalytics.Domain.Model.Commands;
using Dev_ilTeam.Hera.Platform.EnergyAnalytics.Domain.Model.ValueObjects;

namespace Dev_ilTeam.Hera.Platform.EnergyAnalytics.Domain.Model.Aggregates;

public partial class ConsumptionReport
{
    public ConsumptionReport()
    {
        DeviceName = string.Empty;
        Period = string.Empty;
        Measurement = new EnergyMeasurement();
    }

    public ConsumptionReport(string deviceName, string period, decimal kilowattHours, decimal cost) : this()
    {
        DeviceName = deviceName;
        Period = period;
        Measurement = new EnergyMeasurement(kilowattHours, cost);
    }

    public ConsumptionReport(RecordConsumptionCommand command)
        : this(command.DeviceName, command.Period, command.KilowattHours, command.Cost)
    {
    }

    public int Id { get; }
    public string DeviceName { get; private set; }
    public string Period { get; private set; }
    public EnergyMeasurement Measurement { get; private set; }

    public decimal KilowattHours => Measurement.KilowattHours;
    public decimal Cost => Measurement.Cost;
}