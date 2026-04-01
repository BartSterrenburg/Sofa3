using Sofa3.Domain;

namespace Sofa3.Domain.SprintLifecycle.States;

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

