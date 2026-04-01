using Sofa3.Domain;

namespace Sofa3.Domain.SprintLifecycle.States;

public sealed class ReleasedSprintState : SprintStateBase
{
    public override string Name => "Released";

    public override void Close(Sprint sprint)
    {
        sprint.MoveTo(new ClosedSprintState());
    }
}

