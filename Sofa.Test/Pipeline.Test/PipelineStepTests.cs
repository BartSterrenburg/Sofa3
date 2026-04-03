using Sofa3.Domain.Pipeline;
using Sofa3.Domain.Pipeline.Enumerations;

namespace TestProject1.Pipeline.Test;

public class PipelineStepTests
{
    [Test]
    public void PipelineStep_wordt_succesvol_aangemaakt_met_geldige_waarden()
    {
        var step = new PipelineStep("Build", 1, PipelineStepType.BUILD);

        Assert.Multiple(() =>
        {
            Assert.That(step.StepId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(step.Name, Is.EqualTo("Build"));
            Assert.That(step.Order, Is.EqualTo(1));
            Assert.That(step.Type, Is.EqualTo(PipelineStepType.BUILD));
        });
    }

    [Test]
    public void PipelineStep_vereist_een_geldige_naam_bij_leeg()
    {
        static void CreateStep() => _ = new PipelineStep("", 0, PipelineStepType.BUILD);

        var ex = Assert.Throws<ArgumentException>(CreateStep);

        Assert.Multiple(() =>
        {
            Assert.That(ex!.ParamName, Is.EqualTo("name"));
            Assert.That(ex.Message, Does.StartWith("Step name is required."));
        });
    }

    [Test]
    public void PipelineStep_vereist_een_geldige_naam_bij_whitespace()
    {
        static void CreateStep() => _ = new PipelineStep("   ", 0, PipelineStepType.BUILD);

        var ex = Assert.Throws<ArgumentException>(CreateStep);

        Assert.Multiple(() =>
        {
            Assert.That(ex!.ParamName, Is.EqualTo("name"));
            Assert.That(ex.Message, Does.StartWith("Step name is required."));
        });
    }

    [Test]
    public void PipelineStep_vereist_een_geldige_order()
    {
        static void CreateStep() => _ = new PipelineStep("Build", -1, PipelineStepType.BUILD);

        var ex = Assert.Throws<ArgumentOutOfRangeException>(CreateStep);

        Assert.Multiple(() =>
        {
            Assert.That(ex!.ParamName, Is.EqualTo("order"));
            Assert.That(ex.Message, Does.StartWith("Order must be zero or greater."));
        });
    }

    [Test]
    public void Execute_gebruikt_de_factory_en_levert_een_succesvolle_step_execution_op()
    {
        var step = new PipelineStep("Build", 0, PipelineStepType.BUILD);
        var context = new PipelineExecution();

        var execution = step.Execute(context);

        Assert.Multiple(() =>
        {
            Assert.That(execution.Status, Is.EqualTo(ExecutionStatus.SUCCEEDED));
            Assert.That(execution.StartedAt, Is.Not.Null);
            Assert.That(execution.FinishedAt, Is.Not.Null);
            Assert.That(execution.LogMessage, Is.EqualTo("Build step 'Build' executed."));
        });
    }

    [Test]
    public void Execute_vereist_een_geldige_context()
    {
        var step = new PipelineStep("Build", 0, PipelineStepType.BUILD);

        var ex = Assert.Throws<ArgumentNullException>(() => step.Execute(null!));

        Assert.That(ex!.ParamName, Is.EqualTo("context"));
    }
}
