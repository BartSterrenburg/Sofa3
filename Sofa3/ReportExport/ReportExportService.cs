using System;
using SprintReportModel = global::Sofa3.Domain.SprintReport.SprintReport;

namespace Sofa3.Domain.ReportExport;

public sealed class ReportExportService
{
    private ReportExportService()
    {
    }


    public static ExportedReport Export(SprintReportModel report, ExportFormat format)
    {
        ArgumentNullException.ThrowIfNull(report);

        var exporter = ReportExporterFactory.Create(format);
        return exporter.Export(report);
    }
}


