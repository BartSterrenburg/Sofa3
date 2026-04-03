using Sofa3.Domain.ReportExport;

namespace TestProject1.ReportExport.Test;

public class ReportExporterFactoryTests
{
    [Test]
    public void Create_geeft_pdf_exporter_terug_voor_pdf_format()
    {
        var exporter = ReportExporterFactory.Create(ExportFormat.PDF);

        Assert.That(exporter, Is.TypeOf<PdfReportExporter>());
    }

    [Test]
    public void Create_geeft_png_exporter_terug_voor_png_format()
    {
        var exporter = ReportExporterFactory.Create(ExportFormat.PNG);

        Assert.That(exporter, Is.TypeOf<PngReportExporter>());
    }

    [Test]
    public void Create_gooit_NotSupportedException_bij_onbekend_format()
    {
        var ex = Assert.Throws<NotSupportedException>(() =>
            ReportExporterFactory.Create((ExportFormat)999));

        Assert.That(ex!.Message, Does.StartWith("Unsupported export format:"));
    }
}

