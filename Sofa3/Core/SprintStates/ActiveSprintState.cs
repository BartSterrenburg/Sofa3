
using Sofa3.Domain.Core.States;

namespace Sofa3.Domain.Core.SprintStates;

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

