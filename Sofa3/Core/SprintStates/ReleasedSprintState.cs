
using Sofa3.Domain.Core.States;

namespace Sofa3.Domain.Core.SprintStates;

public sealed class ReleasedSprintState : SprintStateBase
{
    public override string Name => "Released";

    public override void Close(Sprint sprint)
    {
        sprint.MoveTo(new ClosedSprintState());
    }
}

