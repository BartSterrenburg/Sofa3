using SprintReportModel = Sofa3.Domain.SprintReport.SprintReport;

namespace TestProject1.SprintReport.Test;

public class SprintReportTests
{
    [Test]
    public void SprintReport_wordt_succesvol_aangemaakt_met_standaardwaarden()
    {
        var before = DateTime.UtcNow;

        var report = new SprintReportModel();

        var after = DateTime.UtcNow;

        Assert.Multiple(() =>
        {
            Assert.That(report.ReportId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(report.GeneratedAtUtc, Is.InRange(before, after));
            Assert.That(report.GeneratedAt, Is.EqualTo(report.GeneratedAtUtc));
            Assert.That(report.SprintName, Is.EqualTo(string.Empty));
            Assert.That(report.ProjectName, Is.EqualTo(string.Empty));
            Assert.That(report.Version, Is.EqualTo(string.Empty));
            Assert.That(report.TeamComposition, Is.EqualTo(string.Empty));
            Assert.That(report.EffortPerDeveloper, Is.EqualTo(string.Empty));
            Assert.That(report.BurndownSummary, Is.EqualTo(string.Empty));
            Assert.That(report.LayoutName, Is.EqualTo(string.Empty));
            Assert.That(report.Content, Is.EqualTo(string.Empty));
            Assert.That(report.HeaderText, Is.EqualTo(string.Empty));
            Assert.That(report.FooterText, Is.EqualTo(string.Empty));
        });
    }

    [Test]
    public void SprintReport_laat_eigenschappen_aanpassen()
    {
        var report = new SprintReportModel
        {
            SprintId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            SprintName = "Sprint 1",
            ProjectName = "Project A",
            Version = "1.0.0",
            TeamComposition = "Team A",
            EffortPerDeveloper = "8h",
            BurndownSummary = "Stable",
            LayoutName = "Default",
            Content = "Summary",
            HeaderText = "Header",
            FooterText = "Footer"
        };

        Assert.Multiple(() =>
        {
            Assert.That(report.SprintId, Is.EqualTo(Guid.Parse("11111111-1111-1111-1111-111111111111")));
            Assert.That(report.SprintName, Is.EqualTo("Sprint 1"));
            Assert.That(report.ProjectName, Is.EqualTo("Project A"));
            Assert.That(report.Version, Is.EqualTo("1.0.0"));
            Assert.That(report.TeamComposition, Is.EqualTo("Team A"));
            Assert.That(report.EffortPerDeveloper, Is.EqualTo("8h"));
            Assert.That(report.BurndownSummary, Is.EqualTo("Stable"));
            Assert.That(report.LayoutName, Is.EqualTo("Default"));
            Assert.That(report.Content, Is.EqualTo("Summary"));
            Assert.That(report.HeaderText, Is.EqualTo("Header"));
            Assert.That(report.FooterText, Is.EqualTo("Footer"));
        });
    }
}

