using System;
using Sofa3.Domain;

namespace Sofa3.Domain.SprintReport;

public interface ReportLayoutStrategy
{
    string Name { get; }

    string Format(Sprint sprint);

    void ApplyTo(SprintReport report)
    {
        ArgumentNullException.ThrowIfNull(report);
        report.LayoutName = Name;
    }
}

