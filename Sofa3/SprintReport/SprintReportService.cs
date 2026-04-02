using Sofa3.Domain.Core;

namespace Sofa3.Domain.SprintReport;

public sealed class SprintReportService
{
    public SprintReport GenerateFor(Sprint sprint, ReportLayoutStrategy layout)
    {
        ArgumentNullException.ThrowIfNull(sprint);
        ArgumentNullException.ThrowIfNull(layout);

        var report = new SprintReport
        {
            SprintId = sprint.SprintId,
            SprintName = sprint.Name,
            ProjectName = string.Empty,
            Version = "1.0.0",
            TeamComposition = string.Empty,
            EffortPerDeveloper = string.Empty,
            BurndownSummary = string.Empty,
            LayoutName = layout.Name,
            Content = layout.Format(sprint)
        };

        layout.ApplyTo(report);
        return report;
    }
}


