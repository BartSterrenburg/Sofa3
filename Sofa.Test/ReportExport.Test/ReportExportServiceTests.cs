using Sofa3.Domain.ReportExport;
using SprintReportModel = global::Sofa3.Domain.SprintReport.SprintReport;

namespace TestProject1.ReportExport.Test;

public class ReportExportServiceTests
{
    [Test]
    public void Export_vereist_report_bij_null()
    {
        var ex = Assert.Throws<ArgumentNullException>(() =>
            ReportExportService.Export(null!, ExportFormat.PDF));

        Assert.That(ex!.ParamName, Is.EqualTo("report"));
    }

    [Test]
    public void Export_geeft_pdf_export_terug_met_juist_contract()
    {
        var report = CreateReport();

        var exported = ReportExportService.Export(report, ExportFormat.PDF);

        Assert.Multiple(() =>
        {
            Assert.That(exported.FileName, Is.EqualTo("Sprint-1.pdf"));
            Assert.That(exported.MimeType, Is.EqualTo("application/pdf"));
            Assert.That(exported.Data, Is.Not.Empty);
        });
    }

    [Test]
    public void Export_geeft_png_export_terug_met_juist_contract()
    {
        var report = CreateReport();

        var exported = ReportExportService.Export(report, ExportFormat.PNG);

        Assert.Multiple(() =>
        {
            Assert.That(exported.FileName, Is.EqualTo("Sprint-1.png"));
            Assert.That(exported.MimeType, Is.EqualTo("image/png"));
            Assert.That(exported.Data, Is.Not.Empty);
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

