using System.Text;
using Sofa3.Domain.ReportExport;
using SprintReportModel = global::Sofa3.Domain.SprintReport.SprintReport;

namespace TestProject1.ReportExport.Test;

public class PngReportExporterTests
{
    [Test]
    public void Export_vereist_report_bij_null()
    {
        var exporter = new PngReportExporter();

        var ex = Assert.Throws<ArgumentNullException>(() => exporter.Export(null!));

        Assert.That(ex!.ParamName, Is.EqualTo("report"));
    }

    [Test]
    public void Export_maakt_een_geldige_png_export_met_bestandsnaam_en_mimetype()
    {
        var exporter = new PngReportExporter();
        var report = CreateReport();

        var exported = exporter.Export(report);

        Assert.Multiple(() =>
        {
            Assert.That(exported.FileName, Is.EqualTo("Sprint-1.png"));
            Assert.That(exported.MimeType, Is.EqualTo("image/png"));
            Assert.That(exported.Data, Is.Not.Empty);
            Assert.That(exported.Data.Take(8), Is.EqualTo(new byte[] { 137, 80, 78, 71, 13, 10, 26, 10 }));
        });
    }

    [Test]
    public void Export_bevat_comment_metadata_met_sprint_en_project()
    {
        var exporter = new PngReportExporter();
        var report = CreateReport();

        var exported = exporter.Export(report);
        var pngText = Encoding.ASCII.GetString(exported.Data);

        Assert.Multiple(() =>
        {
            Assert.That(pngText, Does.Contain("Comment"));
            Assert.That(pngText, Does.Contain("Sprint: Sprint 1"));
            Assert.That(pngText, Does.Contain("Project: Project A"));
        });
    }

    [Test]
    public void Export_is_deterministisch_voor_hetzelfde_report()
    {
        var exporter = new PngReportExporter();
        var report = CreateReport();

        var firstExport = exporter.Export(report);
        var secondExport = exporter.Export(report);

        Assert.That(secondExport.Data, Is.EqualTo(firstExport.Data));
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

