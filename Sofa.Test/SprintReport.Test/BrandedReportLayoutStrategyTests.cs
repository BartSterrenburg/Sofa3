using Sofa3.Domain.Core;
using Sofa3.Domain.SprintReport;
using SprintReportModel = Sofa3.Domain.SprintReport.SprintReport;

namespace TestProject1.SprintReport.Test;

public class BrandedReportLayoutStrategyTests
{
    [Test]
    public void Name_is_Branded()
    {
        var strategy = new BrandedReportLayoutStrategy();

        Assert.That(strategy.Name, Is.EqualTo("Branded"));
    }

    [Test]
    public void Format_returns_expected_text()
    {
        var strategy = new BrandedReportLayoutStrategy();
        var sprint = new Sprint(Guid.NewGuid(), "Sprint 1");

        var formatted = strategy.Format(sprint);

        Assert.That(formatted, Is.EqualTo("Sofa3 sprint report for Sprint 1"));
    }

    [Test]
    public void ApplyTo_sets_layout_header_and_versioned_footer()
    {
        var strategy = new BrandedReportLayoutStrategy();
        var report = new SprintReportModel { Version = "1.2.3" };

        strategy.ApplyTo(report);

        Assert.Multiple(() =>
        {
            Assert.That(report.LayoutName, Is.EqualTo("Branded"));
            Assert.That(report.HeaderText, Is.EqualTo("Sofa3 | Sprint Report"));
            Assert.That(report.FooterText, Is.EqualTo("Sofa3 | Version v1.2.3"));
        });
    }

    [Test]
    public void ApplyTo_uses_v1_when_version_is_empty()
    {
        var strategy = new BrandedReportLayoutStrategy();
        var report = new SprintReportModel();

        strategy.ApplyTo(report);

        Assert.That(report.FooterText, Is.EqualTo("Sofa3 | Version v1"));
    }

    [Test]
    public void Format_vereist_een_geldige_sprint()
    {
        var strategy = new BrandedReportLayoutStrategy();

        var ex = Assert.Throws<ArgumentNullException>(() => strategy.Format(null!));

        Assert.That(ex!.ParamName, Is.EqualTo("sprint"));
    }

    [Test]
    public void ApplyTo_vereist_een_geldige_report()
    {
        var strategy = new BrandedReportLayoutStrategy();

        var ex = Assert.Throws<ArgumentNullException>(() => strategy.ApplyTo(null!));

        Assert.That(ex!.ParamName, Is.EqualTo("report"));
    }
}

