namespace DevilTeam.Hera.Platform.Automation.Interfaces.REST.Controllers;

using Application.Internal.CommandServices;
using Application.Internal.Commands;
using Application.Internal.Queries;
using Application.Internal.QueryServices;
using Microsoft.AspNetCore.Mvc;
using Resources;
using Transform;

/// <summary>
/// REST controller for AutomationRule endpoints.
/// </summary>
[ApiController]
[Route("api/v1/automation-rules")]
[Produces("application/json")]
public class AutomationRulesController(
    AutomationRuleCommandService commandService,
    AutomationRuleQueryService queryService) : ControllerBase
{
    /// <summary>Creates a new automation rule.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(AutomationRuleResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAutomationRule(
        [FromBody] CreateAutomationRuleResource resource,
        CancellationToken cancellationToken)
    {
        var command = CreateAutomationRuleCommandFromResourceAssembler
            .ToCommandFromResource(resource);

        var rule = await commandService.Handle(command, cancellationToken);
        if (rule is null) return BadRequest();

        var result = AutomationRuleResourceFromEntityAssembler.ToResourceFromEntity(rule);
        return CreatedAtAction(nameof(GetAutomationRuleById), new { id = result.Id }, result);
    }

    /// <summary>Returns all automation rules for a user.</summary>
    [HttpGet("user/{userId:int}")]
    [ProducesResponseType(typeof(IEnumerable<AutomationRuleResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAutomationRulesByUserId(
        int userId,
        CancellationToken cancellationToken)
    {
        var query = new GetAutomationRulesByUserIdQuery(userId);
        var rules = await queryService.Handle(query, cancellationToken);
        var result = rules.Select(AutomationRuleResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(result);
    }

    /// <summary>Returns a single automation rule by id.</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(AutomationRuleResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAutomationRuleById(
        int id,
        CancellationToken cancellationToken)
    {
        var query = new GetAutomationRuleByIdQuery(id);
        var rule = await queryService.Handle(query, cancellationToken);
        if (rule is null) return NotFound();

        var result = AutomationRuleResourceFromEntityAssembler.ToResourceFromEntity(rule);
        return Ok(result);
    }

    /// <summary>Updates an existing automation rule.</summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(AutomationRuleResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAutomationRule(
        int id,
        [FromBody] UpdateAutomationRuleResource resource,
        CancellationToken cancellationToken)
    {
        var command = UpdateAutomationRuleCommandFromResourceAssembler
            .ToCommandFromResource(id, resource);

        var rule = await commandService.Handle(command, cancellationToken);
        if (rule is null) return NotFound();

        var result = AutomationRuleResourceFromEntityAssembler.ToResourceFromEntity(rule);
        return Ok(result);
    }

    /// <summary>Activates or deactivates an automation rule.</summary>
    [HttpPatch("{id:int}/activate")]
    [ProducesResponseType(typeof(AutomationRuleResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ToggleAutomationRule(
        int id,
        [FromQuery] bool activate,
        CancellationToken cancellationToken)
    {
        var rule = await commandService.HandleToggle(id, activate, cancellationToken);
        if (rule is null) return NotFound();

        var result = AutomationRuleResourceFromEntityAssembler.ToResourceFromEntity(rule);
        return Ok(result);
    }

    /// <summary>Manually executes an automation rule.</summary>
    [HttpPost("{id:int}/execute")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ExecuteAutomationRule(
        int id,
        CancellationToken cancellationToken)
    {
        var command = new ExecuteAutomationRuleCommand(id);
        var success = await commandService.Handle(command, cancellationToken);
        if (!success) return NotFound();

        return Ok(new { message = "Automation rule executed successfully." });
    }

    /// <summary>Deletes an automation rule.</summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAutomationRule(
        int id,
        CancellationToken cancellationToken)
    {
        var success = await commandService.HandleDelete(id, cancellationToken);
        if (!success) return NotFound();

        return NoContent();
    }
}