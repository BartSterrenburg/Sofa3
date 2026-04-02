using System;
using Sofa3.Domain.Core;

namespace Sofa3.Domain.SprintReport;

public sealed class BrandedReportLayoutStrategy : IReportLayoutStrategy
{
    private const string BrandName = "Sofa3";

    public string Name => "Branded";

    public string Format(Sprint sprint)
    {
        ArgumentNullException.ThrowIfNull(sprint);
        return $"{BrandName} sprint report for {sprint.Name}";
    }

    public void ApplyTo(SprintReport report)
    {
        ArgumentNullException.ThrowIfNull(report);
        report.LayoutName = Name;
        report.HeaderText = $"{BrandName} | Sprint Report";
        report.FooterText = $"{BrandName} | Version {LabelVersion(report.Version)}";
    }

    private static string LabelVersion(string version)
    {
        return string.IsNullOrWhiteSpace(version) ? "v1" : $"v{version}";
    }
}

