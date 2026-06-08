namespace DevilTeam.Hera.Platform.Automation.Application.Internal.CommandServices;

using Commands;
using Domain.Model.Aggregates;
using Domain.Model.ValueObjects;
using Domain.Repositories;
using Shared.Domain.Repositories;

/// <summary>
/// Handles all write operations for AutomationRule.
/// </summary>
public class AutomationRuleCommandService(
    IAutomationRuleRepository automationRuleRepository,
    IUnitOfWork unitOfWork)
{
    /// <summary>Creates a new automation rule and persists it.</summary>
    public async Task<AutomationRule?> Handle(CreateAutomationRuleCommand command,
        CancellationToken cancellationToken = default)
    {
        var trigger = new Trigger(
            command.TriggerType,
            command.ScheduledTime,
            command.DeviceId,
            command.ConditionValue);

        var action = new AutomationAction(
            command.ActionType,
            command.TargetDeviceId,
            command.ActionValue);

        var rule = new AutomationRule(
            command.Name,
            command.Description,
            command.HomeId,
            command.UserId,
            trigger,
            action);

        await automationRuleRepository.AddAsync(rule, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        return rule;
    }

    /// <summary>Updates an existing automation rule.</summary>
    public async Task<AutomationRule?> Handle(UpdateAutomationRuleCommand command,
        CancellationToken cancellationToken = default)
    {
        var rule = await automationRuleRepository.FindByIdAsync(command.Id, cancellationToken);
        if (rule is null) return null;

        var trigger = new Trigger(
            command.TriggerType,
            command.ScheduledTime,
            command.DeviceId,
            command.ConditionValue);

        var action = new AutomationAction(
            command.ActionType,
            command.TargetDeviceId,
            command.ActionValue);

        rule.Update(command.Name, command.Description, trigger, action);

        automationRuleRepository.Update(rule);
        await unitOfWork.CompleteAsync(cancellationToken);

        return rule;
    }

    /// <summary>Executes (triggers) an automation rule manually.</summary>
    public async Task<bool> Handle(ExecuteAutomationRuleCommand command,
        CancellationToken cancellationToken = default)
    {
        var rule = await automationRuleRepository.FindByIdAsync(command.RuleId, cancellationToken);
        if (rule is null || !rule.IsActive) return false;

        // Execution logic is simulated at this stage.
        // In a real scenario this would dispatch the action to DevicesManagement.
        await unitOfWork.CompleteAsync(cancellationToken);

        return true;
    }

    /// <summary>Activates or deactivates a rule.</summary>
    public async Task<AutomationRule?> HandleToggle(int ruleId, bool activate,
        CancellationToken cancellationToken = default)
    {
        var rule = await automationRuleRepository.FindByIdAsync(ruleId, cancellationToken);
        if (rule is null) return null;

        if (activate) rule.Activate();
        else rule.Deactivate();

        automationRuleRepository.Update(rule);
        await unitOfWork.CompleteAsync(cancellationToken);

        return rule;
    }

    /// <summary>Deletes an automation rule.</summary>
    public async Task<bool> HandleDelete(int ruleId,
        CancellationToken cancellationToken = default)
    {
        var rule = await automationRuleRepository.FindByIdAsync(ruleId, cancellationToken);
        if (rule is null) return false;

        automationRuleRepository.Remove(rule);
        await unitOfWork.CompleteAsync(cancellationToken);

        return true;
    }
}