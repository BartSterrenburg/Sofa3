using System.Text;
using Sofa3.Domain;
using Sofa3.Domain.Notification;
using Sofa3.Domain.ReportExport;
using SprintReportModel = global::Sofa3.Domain.SprintReport.SprintReport;

namespace Sofa3.Tests;

public class ReportExportTests
{
    [Fact]
    public void Factory_returns_pdf_exporter_for_pdf_format()
    {
        var factory = new ReportExporterFactory();

        var exporter = factory.Create(ExportFormat.PDF);

        Assert.IsType<PdfReportExporter>(exporter);
    }

    [Fact]
    public void Factory_returns_png_exporter_for_png_format()
    {
        var factory = new ReportExporterFactory();

        var exporter = factory.Create(ExportFormat.PNG);

        Assert.IsType<PngReportExporter>(exporter);
    }

    [Fact]
    public void Pdf_report_exporter_creates_pdf_export()
    {
        var exporter = new PdfReportExporter();
        var report = CreateReport();

        var exported = exporter.Export(report);

        Assert.Equal("Sprint-1.pdf", exported.FileName);
        Assert.Equal("application/pdf", exported.MimeType);
        Assert.NotEmpty(exported.Data);
        var pdfText = Encoding.ASCII.GetString(exported.Data);
        Assert.StartsWith("%PDF-", pdfText);
        Assert.Contains("Sofa3 Sprint Report", pdfText);
        Assert.Contains("Sprint: Sprint 1", pdfText);
    }

    [Fact]
    public void Png_report_exporter_creates_png_export()
    {
        var exporter = new PngReportExporter();
        var report = CreateReport();

        var exported = exporter.Export(report);

        Assert.Equal("Sprint-1.png", exported.FileName);
        Assert.Equal("image/png", exported.MimeType);
        Assert.NotEmpty(exported.Data);
        Assert.True(exported.Data.Take(8).SequenceEqual(new byte[] { 137, 80, 78, 71, 13, 10, 26, 10 }));

        var pngText = Encoding.ASCII.GetString(exported.Data);
        Assert.Contains("Comment", pngText);
        Assert.Contains("Sprint: Sprint 1", pngText);
    }

    [Fact]
    public void Report_export_service_uses_factory_and_exporter()
    {
        var service = new ReportExportService();
        var report = CreateReport();

        var exported = service.Export(report, ExportFormat.PNG);

        Assert.Equal("Sprint-1.png", exported.FileName);
        Assert.Equal("image/png", exported.MimeType);
        Assert.NotEmpty(exported.Data);
    }

    private static SprintReportModel CreateReport()
    {
        return new SprintReportModel
        {
            SprintId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            SprintName = "Sprint 1",
            ProjectName = "Project A",
            Version = "1.0.0",
            TeamComposition = "Team A",
            EffortPerDeveloper = "8h",
            BurndownSummary = "Stable",
            LayoutName = "Default",
            Content = "Sprint summary",
            HeaderText = "Header",
            FooterText = "Footer"
        };
    }
}



