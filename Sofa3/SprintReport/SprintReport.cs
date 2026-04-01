using System;

namespace Sofa3.Domain.SprintReport;

public sealed class SprintReport
{
    public Guid ReportId { get; } = Guid.NewGuid();
    public DateTime GeneratedAtUtc { get; } = DateTime.UtcNow;
    public DateTime GeneratedAt => GeneratedAtUtc;

    public Guid SprintId { get; set; }
    public string SprintName { get; set; } = string.Empty;
    public string ProjectName { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public string TeamComposition { get; set; } = string.Empty;
    public string EffortPerDeveloper { get; set; } = string.Empty;
    public string BurndownSummary { get; set; } = string.Empty;

    public string LayoutName { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string HeaderText { get; set; } = string.Empty;
    public string FooterText { get; set; } = string.Empty;
}

