using Sofa3.Domain.Core;

namespace Sofa3.Domain.SprintLifecycle.States;

public sealed class ReleasingSprintState : SprintStateBase
{
    public override string Name => "Releasing";

    public override void ReleaseSucceeded(Sprint sprint)
    {
        sprint.MoveTo(new ReleasedSprintState());
    }

    public override void ReleaseFailed(Sprint sprint)
    {
        sprint.MoveTo(new FailedSprintState());
    }
}

