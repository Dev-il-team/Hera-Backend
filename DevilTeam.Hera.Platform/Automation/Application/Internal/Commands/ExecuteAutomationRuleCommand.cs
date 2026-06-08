namespace DevilTeam.Hera.Platform.Automation.Application.Internal.Commands;

/// <summary>
/// Command to manually trigger the execution of an automation rule.
/// </summary>
public record ExecuteAutomationRuleCommand(int RuleId);