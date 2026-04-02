
namespace Sofa3.Domain.Core.States;

public sealed class ReleasedSprintState : SprintStateBase
{
    public override string Name => "Released";

    public override void Close(Sprint sprint)
    {
        sprint.MoveTo(new ClosedSprintState());
    }
}

