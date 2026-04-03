using Sofa3.Domain.Core;
using Sofa3.Domain.SprintReport;
using SprintReportModel = Sofa3.Domain.SprintReport.SprintReport;

namespace TestProject1.SprintReport.Test;

public class SprintReportServiceTests
{
    private sealed class FakeLayoutStrategy : IReportLayoutStrategy
    {
        public string Name => "Plain text";

        public bool FormatCalled { get; private set; }
        public bool ApplyToCalled { get; private set; }

        public string Format(Sprint sprint)
        {
            FormatCalled = true;
            return $"Report for {sprint.Name}";
        }

        public void ApplyTo(SprintReportModel report)
        {
            ApplyToCalled = true;
            report.LayoutName = Name;
            report.HeaderText = "Header";
            report.FooterText = "Footer";
        }
    }

    [Test]
    public void GenerateFor_creates_report_from_sprint_and_layout_strategy()
    {
        var sprint = new Sprint(Guid.Parse("11111111-1111-1111-1111-111111111111"), "Sprint 1");
        var layout = new FakeLayoutStrategy();

        var report = SprintReportService.GenerateFor(sprint, layout);

        Assert.Multiple(() =>
        {
            Assert.That(layout.FormatCalled, Is.True);
            Assert.That(layout.ApplyToCalled, Is.True);
            Assert.That(report.SprintId, Is.EqualTo(sprint.SprintId));
            Assert.That(report.SprintName, Is.EqualTo(sprint.Name));
            Assert.That(report.LayoutName, Is.EqualTo(layout.Name));
            Assert.That(report.Content, Is.EqualTo("Report for Sprint 1"));
            Assert.That(report.Version, Is.EqualTo("1.0.0"));
            Assert.That(report.HeaderText, Is.EqualTo("Header"));
            Assert.That(report.FooterText, Is.EqualTo("Footer"));
        });
    }

    [Test]
    public void GenerateFor_vereist_een_geldige_sprint()
    {
        var layout = new FakeLayoutStrategy();

        var ex = Assert.Throws<ArgumentNullException>(() => SprintReportService.GenerateFor(null!, layout));

        Assert.That(ex!.ParamName, Is.EqualTo("sprint"));
    }

    [Test]
    public void GenerateFor_vereist_een_geldige_layout()
    {
        var sprint = new Sprint(Guid.NewGuid(), "Sprint 1");

        var ex = Assert.Throws<ArgumentNullException>(() => SprintReportService.GenerateFor(sprint, null!));

        Assert.That(ex!.ParamName, Is.EqualTo("layout"));
    }
}

