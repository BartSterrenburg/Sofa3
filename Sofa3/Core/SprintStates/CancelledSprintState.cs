using Sofa3.Domain.Core.States;

namespace Sofa3.Domain.Core.SprintStates;

public sealed class CancelledSprintState : SprintStateBase
{
    public override string Name => "Cancelled";

    public override void Close(Sprint sprint)
    {
        sprint.MoveTo(new ClosedSprintState());
    }
}

