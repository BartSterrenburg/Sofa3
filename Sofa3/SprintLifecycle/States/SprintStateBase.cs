using Sofa3.Domain;

namespace Sofa3.Domain.SprintLifecycle.States;

public abstract class SprintStateBase : SprintState
{
    public abstract string Name { get; }

    public virtual void Start(Sprint sprint) => throw InvalidTransition(nameof(Start));
    public virtual void Finish(Sprint sprint) => throw InvalidTransition(nameof(Finish));
    public virtual void StartRelease(Sprint sprint) => throw InvalidTransition(nameof(StartRelease));
    public virtual void ReleaseSucceeded(Sprint sprint) => throw InvalidTransition(nameof(ReleaseSucceeded));
    public virtual void ReleaseFailed(Sprint sprint) => throw InvalidTransition(nameof(ReleaseFailed));
    public virtual void Close(Sprint sprint) => throw InvalidTransition(nameof(Close));

    protected InvalidOperationException InvalidTransition(string action)
    {
        return new InvalidOperationException($"Action '{action}' is not allowed while sprint is in '{Name}' state.");
    }
}

