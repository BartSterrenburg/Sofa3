using Sofa3.Domain;
using Sofa3.Domain.Notification;
using Sofa3.Domain.SprintReport;

namespace Sofa3.Tests;

public class SprintReportServiceTests
{
    [Fact]
    public void GenerateFor_creates_report_from_layout_strategy()
    {
        var service = new SprintReportService();
        var sprint = new Sprint(Guid.Parse("11111111-1111-1111-1111-111111111111"), "Sprint 1", new DomainEventPublisher());
        var layout = new FakeLayoutStrategy();

        var report = service.GenerateFor(sprint, layout);

        Assert.Equal(sprint.SprintId, report.SprintId);
        Assert.Equal(sprint.Name, report.SprintName);
        Assert.Equal(layout.Name, report.LayoutName);
        Assert.Equal("Report for Sprint 1", report.Content);
        Assert.True(report.GeneratedAtUtc <= DateTime.UtcNow);
    }

    [Fact]
    public void GenerateFor_throws_when_sprint_is_null()
    {
        var service = new SprintReportService();
        var layout = new FakeLayoutStrategy();

        Assert.Throws<ArgumentNullException>(() => service.GenerateFor(null!, layout));
    }

    [Fact]
    public void GenerateFor_throws_when_layout_is_null()
    {
        var service = new SprintReportService();
        var sprint = new Sprint(Guid.NewGuid(), "Sprint 1", new DomainEventPublisher());

        Assert.Throws<ArgumentNullException>(() => service.GenerateFor(sprint, null!));
    }

    private sealed class FakeLayoutStrategy : ReportLayoutStrategy
    {
        public string Name => "Plain text";

        public string Format(Sprint sprint) => $"Report for {sprint.Name}";
    }
}

