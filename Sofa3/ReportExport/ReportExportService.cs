using System;
using SprintReportModel = global::Sofa3.Domain.SprintReport.SprintReport;

namespace Sofa3.Domain.ReportExport;

public sealed class ReportExportService
{
    private readonly ReportExporterFactory _factory;

    public ReportExportService()
    {
        _factory = new ReportExporterFactory();
    }

    //public ReportExportService(ReportExporterFactory factory)
    //{
    //    _factory = factory ?? throw new ArgumentNullException(nameof(factory));
    //}

    public static ExportedReport Export(SprintReportModel report, ExportFormat format)
    {
        ArgumentNullException.ThrowIfNull(report);

        var exporter = ReportExporterFactory.Create(format);
        return exporter.Export(report);
    }
}


