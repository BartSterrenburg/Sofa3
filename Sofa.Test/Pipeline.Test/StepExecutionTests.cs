using Sofa3.Domain.Pipeline;
using Sofa3.Domain.Pipeline.Enumerations;

namespace TestProject1.Pipeline.Test;

public class StepExecutionTests
{
    [Test]
    public void StepExecution_wordt_succesvol_aangemaakt_met_standaardwaarden()
    {
        var execution = new StepExecution();

        Assert.Multiple(() =>
        {
            Assert.That(execution.StepExecutionId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(execution.Status, Is.EqualTo(ExecutionStatus.PENDING));
            Assert.That(execution.StartedAt, Is.Null);
            Assert.That(execution.FinishedAt, Is.Null);
            Assert.That(execution.LogMessage, Is.EqualTo(string.Empty));
        });
    }

    [Test]
    public void MarkRunning_zet_status_op_running_en_startedAt()
    {
        var execution = new StepExecution();
        var before = DateTime.UtcNow;

        execution.MarkRunning();

        var after = DateTime.UtcNow;

        Assert.Multiple(() =>
        {
            Assert.That(execution.Status, Is.EqualTo(ExecutionStatus.RUNNING));
            Assert.That(execution.StartedAt, Is.InRange(before, after));
        });
    }

    [Test]
    public void MarkSucceeded_zet_status_op_succeeded_en_finishedAt()
    {
        var execution = new StepExecution();
        var before = DateTime.UtcNow;

        execution.MarkSucceeded();

        var after = DateTime.UtcNow;

        Assert.Multiple(() =>
        {
            Assert.That(execution.Status, Is.EqualTo(ExecutionStatus.SUCCEEDED));
            Assert.That(execution.FinishedAt, Is.InRange(before, after));
        });
    }

    [Test]
    public void MarkFailed_zet_status_op_failed_en_finishedAt()
    {
        var execution = new StepExecution();
        var before = DateTime.UtcNow;

        execution.MarkFailed();

        var after = DateTime.UtcNow;

        Assert.Multiple(() =>
        {
            Assert.That(execution.Status, Is.EqualTo(ExecutionStatus.FAILED));
            Assert.That(execution.FinishedAt, Is.InRange(before, after));
        });
    }

    [Test]
    public void SetLogMessage_slaat_message_op_en_vervangt_null_door_lege_string()
    {
        var execution = new StepExecution();

        execution.SetLogMessage(null!);

        Assert.That(execution.LogMessage, Is.EqualTo(string.Empty));

        execution.SetLogMessage("Build completed");

        Assert.That(execution.LogMessage, Is.EqualTo("Build completed"));
    }
}
