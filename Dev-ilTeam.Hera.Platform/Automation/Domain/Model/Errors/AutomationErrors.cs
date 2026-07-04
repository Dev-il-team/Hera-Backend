using Dev_ilTeam.Hera.Platform.Shared.Domain.Model;

namespace Dev_ilTeam.Hera.Platform.Automation.Domain.Model.Errors;

public static class AutomationErrors
{
    public static readonly Error RoutineNotFound =
        new("Automation.RoutineNotFound", "The specified routine was not found.");

    public static readonly Error DuplicateRoutineName =
        new("Automation.DuplicateRoutineName", "A routine with the specified name already exists.");
}
