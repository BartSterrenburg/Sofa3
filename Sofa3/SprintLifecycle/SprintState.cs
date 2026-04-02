using Sofa3.Domain.Core;

namespace Sofa3.Domain.SprintLifecycle;

public interface SprintState
{
    string Name { get; }
    void Start(Sprint sprint);
    void Finish(Sprint sprint);
    void StartRelease(Sprint sprint);
    void ReleaseSucceeded(Sprint sprint);
    void ReleaseFailed(Sprint sprint);
    void Close(Sprint sprint);
}

