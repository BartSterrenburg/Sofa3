namespace Sofa3.Domain.Core;

public interface ISprintState
{
    string Name { get; }
    void Start(Sprint sprint);
    void Finish(Sprint sprint);
    void StartRelease(Sprint sprint);
    void ReleaseSucceeded(Sprint sprint);
    void ReleaseFailed(Sprint sprint);
    void Close(Sprint sprint);
}

