using System;
using SprintReportModel = global::Sofa3.Domain.SprintReport.SprintReport;

namespace Sofa3.Domain.ReportExport;

public sealed class ReportExportService
{
    private readonly ReportExporterFactory _factory;

    public ReportExportService()
        : this(new ReportExporterFactory())
    {
    }

    public ReportExportService(ReportExporterFactory factory)
    {
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
    }

    public ExportedReport Export(SprintReportModel report, ExportFormat format)
    {
        ArgumentNullException.ThrowIfNull(report);

        var exporter = _factory.Create(format);
        return exporter.Export(report);
    }
}


