namespace DevilTeam.Hera.Platform.Automation.Application.Internal.Queries;

/// <summary>
/// Query to retrieve all automation rules belonging to a user.
/// </summary>
public record GetAutomationRulesByUserIdQuery(int UserId);