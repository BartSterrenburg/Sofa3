using Sofa3.Domain.Notification.DomainEvents;
using Sofa3.Domain.Core;
using Sofa3.Domain.Core.SprintStates;

namespace Sofa3.Tests;

public class SprintLifecycleTests
{
    [Fact]
    public void New_sprint_starts_in_concept_state()
    {
        var sprint = CreateSprint();

        Assert.IsType<ConceptSprintState>(sprint.CurrentState);
    }

    [Fact]
    public void Start_moves_concept_to_active()
    {
        var sprint = CreateSprint();

        sprint.Start();

        Assert.IsType<ActiveSprintState>(sprint.CurrentState);
    }

    [Fact]
    public void Finish_moves_active_to_finished()
    {
        var sprint = CreateSprint();
        sprint.Start();

        sprint.Finish();

        Assert.IsType<FinishedSprintState>(sprint.CurrentState);
    }

    [Fact]
    public void ReleaseSucceeded_publishes_sprint_released_event()
    {
        var sprint = CreateSprint();
        sprint.Start();
        sprint.Finish();
        sprint.StartRelease();

        sprint.ReleaseSucceeded();

        var evt = Assert.Single(sprint.DomainEvents);
        Assert.IsType<SprintReleasedEvent>(evt);
        Assert.IsType<ReleasedSprintState>(sprint.CurrentState);
    }

    [Fact]
    public void Release_from_new_sprint_reaches_released_state()
    {
        var sprint = CreateSprint();

        sprint.Release();

        Assert.IsType<ReleasedSprintState>(sprint.CurrentState);
    }

    [Fact]
    public void Invalid_transition_throws_invalid_operation_exception()
    {
        var sprint = CreateSprint();

        Assert.Throws<InvalidOperationException>(() => sprint.StartRelease());
    }

    private static Sprint CreateSprint()
    {
        return new Sprint(Guid.NewGuid(), "Sprint");
    }
}

