using Sofa3.Domain.Core;
using Sofa3.Domain.Core.States;


public sealed class ConceptSprintState : SprintStateBase
{
    public override string Name => "Concept";

    public override void Start(Sprint sprint)
    {
        sprint.MoveTo(new ActiveSprintState());
    }

    public override void Close(Sprint sprint)
    {
        sprint.MoveTo(new CancelledSprintState());
    }
}

