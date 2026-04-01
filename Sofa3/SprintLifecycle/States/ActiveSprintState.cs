using Sofa3.Domain;

namespace Sofa3.Domain.SprintLifecycle.States;

public sealed class ActiveSprintState : SprintStateBase
{
    public override string Name => "Active";

    public override void Finish(Sprint sprint)
    {
        sprint.MoveTo(new FinishedSprintState());
    }

    public override void Close(Sprint sprint)
    {
        sprint.MoveTo(new CancelledSprintState());
    }
}

