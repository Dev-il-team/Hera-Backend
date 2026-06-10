using DevilTeam.Hera.Platform.Automation.Application.Internal.CommandServices;
using DevilTeam.Hera.Platform.Automation.Application.Internal.QueryServices;
using DevilTeam.Hera.Platform.Automation.Domain.Repositories;
using DevilTeam.Hera.Platform.Automation.Infrastructure.Persistence.EFC.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Automation
builder.Services.AddScoped<IAutomationRuleRepository, AutomationRuleRepository>();
builder.Services.AddScoped<AutomationRuleCommandService>();
builder.Services.AddScoped<AutomationRuleQueryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();