using Sofa3.Domain;

namespace Sofa3.Domain.SprintLifecycle.States;

public sealed class CancelledSprintState : SprintStateBase
{
    public override string Name => "Cancelled";

    public override void Close(Sprint sprint)
    {
        sprint.MoveTo(new ClosedSprintState());
    }
}

