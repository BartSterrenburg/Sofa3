using Sofa3.Domain.ReportExport;

namespace TestProject1.ReportExport.Test;

public class ExportedReportTests
{
    [Test]
    public void ExportedReport_wordt_succesvol_aangemaakt_met_geldige_waarden()
    {
        var data = new byte[] { 1, 2, 3 };

        var exportedReport = new ExportedReport("sprint-report.pdf", "application/pdf", data);

        Assert.Multiple(() =>
        {
            Assert.That(exportedReport.FileName, Is.EqualTo("sprint-report.pdf"));
            Assert.That(exportedReport.MimeType, Is.EqualTo("application/pdf"));
            Assert.That(exportedReport.Data, Is.EqualTo(new byte[] { 1, 2, 3 }));
        });
    }

    [Test]
    public void ExportedReport_maakt_een_defensieve_kopie_van_data()
    {
        var data = new byte[] { 1, 2, 3 };

        var exportedReport = new ExportedReport("sprint-report.pdf", "application/pdf", data);
        data[0] = 99;

        Assert.Multiple(() =>
        {
            Assert.That(exportedReport.Data[0], Is.EqualTo(1));
            Assert.That(exportedReport.Data, Is.Not.SameAs(data));
        });
    }

    [Test]
    public void ExportedReport_vereist_een_geldige_fileName_bij_leeg()
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            new ExportedReport("", "application/pdf", new byte[] { 1 }));

        Assert.That(ex!.ParamName, Is.EqualTo("fileName"));
    }

    [Test]
    public void ExportedReport_vereist_een_geldige_fileName_bij_whitespace()
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            new ExportedReport("   ", "application/pdf", new byte[] { 1 }));

        Assert.That(ex!.ParamName, Is.EqualTo("fileName"));
    }

    [Test]
    public void ExportedReport_vereist_een_geldige_fileName_bij_null()
    {
        var ex = Assert.Throws<ArgumentNullException>(() =>
            new ExportedReport(null!, "application/pdf", new byte[] { 1 }));

        Assert.That(ex!.ParamName, Is.EqualTo("fileName"));
    }

    [Test]
    public void ExportedReport_vereist_een_geldige_mimeType_bij_leeg()
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            new ExportedReport("sprint-report.pdf", "", new byte[] { 1 }));

        Assert.That(ex!.ParamName, Is.EqualTo("mimeType"));
    }

    [Test]
    public void ExportedReport_vereist_een_geldige_mimeType_bij_whitespace()
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            new ExportedReport("sprint-report.pdf", "   ", new byte[] { 1 }));

        Assert.That(ex!.ParamName, Is.EqualTo("mimeType"));
    }

    [Test]
    public void ExportedReport_vereist_een_geldige_mimeType_bij_null()
    {
        var ex = Assert.Throws<ArgumentNullException>(() =>
            new ExportedReport("sprint-report.pdf", null!, new byte[] { 1 }));

        Assert.That(ex!.ParamName, Is.EqualTo("mimeType"));
    }

    [Test]
    public void ExportedReport_vereist_data_bij_null()
    {
        var ex = Assert.Throws<ArgumentNullException>(() =>
            new ExportedReport("sprint-report.pdf", "application/pdf", null!));

        Assert.That(ex!.ParamName, Is.EqualTo("data"));
    }
}
