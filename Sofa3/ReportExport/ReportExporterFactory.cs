

using Sofa3.Domain.ReportExport;

namespace Sofa3.Domain.ReportExport;

public static class ReportExporterFactory
{
    public static IReportExporter Create(ExportFormat format)
    {
        return format switch
        {
            ExportFormat.PDF => new PdfReportExporter(),
            ExportFormat.PNG => new PngReportExporter(),
            _ => throw new NotSupportedException($"Unsupported export format: {format}")
        };
    }
}

