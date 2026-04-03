

using Sofa3.Domain.ReportExport;

public sealed class ReportExporterFactory
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

