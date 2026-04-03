using Sofa3.Domain.Pipeline;
using Sofa3.Domain.Pipeline.Enumerations;

namespace TestProject1.Pipeline.Test;

public class PipelineExecutionTests
{
    [Test]
    public void PipelineExecution_wordt_succesvol_aangemaakt_met_standaard_status()
    {
        var execution = new PipelineExecution();

        Assert.Multiple(() =>
        {
            Assert.That(execution.ExecutionId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(execution.Status, Is.EqualTo(ExecutionStatus.PENDING));
            Assert.That(execution.StartedAt, Is.Null);
            Assert.That(execution.FinishedAt, Is.Null);
            Assert.That(execution.StepExecutions, Is.Empty);
        });
    }

    [Test]
    public void Start_zet_status_op_running_en_startedAt()
    {
        var execution = new PipelineExecution();
        var before = DateTime.UtcNow;

        execution.Start();

        var after = DateTime.UtcNow;

        Assert.Multiple(() =>
        {
            Assert.That(execution.Status, Is.EqualTo(ExecutionStatus.RUNNING));
            Assert.That(execution.StartedAt, Is.InRange(before, after));
        });
    }

    [Test]
    public void AddStepExecution_vereist_een_geldige_stepExecution()
    {
        var execution = new PipelineExecution();

        var ex = Assert.Throws<ArgumentNullException>(() => execution.AddStepExecution(null!));

        Assert.That(ex!.ParamName, Is.EqualTo("stepExecution"));
    }

    [Test]
    public void AddStepExecution_slaat_step_execution_op()
    {
        var execution = new PipelineExecution();
        var stepExecution = new StepExecution();

        execution.AddStepExecution(stepExecution);

        Assert.That(execution.StepExecutions, Has.Count.EqualTo(1));
        Assert.That(execution.StepExecutions.First(), Is.SameAs(stepExecution));
    }

    [Test]
    public void MarkSucceeded_zet_status_op_succeeded_en_finishedAt()
    {
        var execution = new PipelineExecution();
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
        var execution = new PipelineExecution();
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
    public void Cancel_zet_status_op_cancelled_en_finishedAt()
    {
        var execution = new PipelineExecution();
        var before = DateTime.UtcNow;

        execution.Cancel();

        var after = DateTime.UtcNow;

        Assert.Multiple(() =>
        {
            Assert.That(execution.Status, Is.EqualTo(ExecutionStatus.CANCELLED));
            Assert.That(execution.FinishedAt, Is.InRange(before, after));
        });
    }
}

