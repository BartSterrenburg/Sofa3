using System.Text;
using Sofa3.Domain.ReportExport;
using SprintReportModel = global::Sofa3.Domain.SprintReport.SprintReport;

namespace TestProject1.ReportExport.Test;

public class PdfReportExporterTests
{
    [Test]
    public void Export_vereist_report_bij_null()
    {
        var exporter = new PdfReportExporter();

        var ex = Assert.Throws<ArgumentNullException>(() => exporter.Export(null!));

        Assert.That(ex!.ParamName, Is.EqualTo("report"));
    }

    [Test]
    public void Export_maakt_een_geldige_pdf_export_met_bestandsnaam_en_mimetype()
    {
        var exporter = new PdfReportExporter();
        var report = CreateReport();

        var exported = exporter.Export(report);

        Assert.Multiple(() =>
        {
            Assert.That(exported.FileName, Is.EqualTo("Sprint-1.pdf"));
            Assert.That(exported.MimeType, Is.EqualTo("application/pdf"));
            Assert.That(exported.Data, Is.Not.Empty);
        });
    }

    [Test]
    public void Export_bevat_verwachte_pdf_header_en_inhoudsregels()
    {
        var exporter = new PdfReportExporter();
        var report = CreateReport();

        var exported = exporter.Export(report);
        var pdfText = Encoding.ASCII.GetString(exported.Data);

        Assert.Multiple(() =>
        {
            Assert.That(pdfText, Does.StartWith("%PDF-"));
            Assert.That(pdfText, Does.Contain("Sofa3 Sprint Report"));
            Assert.That(pdfText, Does.Contain("Sprint: Sprint 1"));
            Assert.That(pdfText, Does.Contain("Project: Project A"));
        });
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

