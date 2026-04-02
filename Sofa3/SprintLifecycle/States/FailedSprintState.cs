using Sofa3.Domain.Core;

namespace Sofa3.Domain.SprintLifecycle.States;

public sealed class FailedSprintState : SprintStateBase
{
    public override string Name => "Failed";

    public override void StartRelease(Sprint sprint)
    {
        sprint.MoveTo(new ReleasingSprintState());
    }

    public override void Close(Sprint sprint)
    {
        sprint.MoveTo(new ClosedSprintState());
    }
}

