
namespace Sofa3.Domain.Core.States;

public sealed class FinishedSprintState : SprintStateBase
{
    public override string Name => "Finished";

    public override void StartRelease(Sprint sprint)
    {
        sprint.MoveTo(new ReleasingSprintState());
    }

    public override void Close(Sprint sprint)
    {
        sprint.MoveTo(new ClosedSprintState());
    }
}

