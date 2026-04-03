using PipelineModel = Sofa3.Domain.Pipeline.Pipeline;
using Sofa3.Domain.Pipeline;
using Sofa3.Domain.Pipeline.Enumerations;

namespace TestProject1.Pipeline.Test;

public class PipelineTests
{
    [Test]
    public void Pipeline_wordt_succesvol_aangemaakt_met_geldige_waarden()
    {
        var pipeline = new PipelineModel("Build pipeline", PipelineTriggerMode.MANUAL);

        Assert.Multiple(() =>
        {
            Assert.That(pipeline.PipelineId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(pipeline.Name, Is.EqualTo("Build pipeline"));
            Assert.That(pipeline.TriggerMode, Is.EqualTo(PipelineTriggerMode.MANUAL));
            Assert.That(pipeline.Steps, Is.Empty);
        });
    }

    [Test]
    public void Pipeline_vereist_een_geldige_naam_bij_leeg()
    {
        static void CreatePipeline() => _ = new PipelineModel("", PipelineTriggerMode.MANUAL);

        var ex = Assert.Throws<ArgumentException>(CreatePipeline);

        Assert.That(ex!.ParamName, Is.EqualTo("name"));
        Assert.That(ex.Message, Does.StartWith("Pipeline name is required."));
    }

    [Test]
    public void Pipeline_vereist_een_geldige_naam_bij_whitespace()
    {
        static void CreatePipeline() => _ = new PipelineModel("   ", PipelineTriggerMode.MANUAL);

        var ex = Assert.Throws<ArgumentException>(CreatePipeline);

        Assert.That(ex!.ParamName, Is.EqualTo("name"));
        Assert.That(ex.Message, Does.StartWith("Pipeline name is required."));
    }

    [Test]
    public void AddStep_slaat_stappen_op_op_volgorde_van_order()
    {
        var pipeline = new PipelineModel("Build pipeline", PipelineTriggerMode.MANUAL);
        var third = new PipelineStep("Deploy", 2, PipelineStepType.DEPLOY);
        var first = new PipelineStep("Sources", 0, PipelineStepType.SOURCES);
        var second = new PipelineStep("Build", 1, PipelineStepType.BUILD);

        pipeline.AddStep(third);
        pipeline.AddStep(first);
        pipeline.AddStep(second);

        Assert.That(pipeline.Steps.Select(step => step.Name), Is.EqualTo(new[] { "Sources", "Build", "Deploy" }));
    }

    [Test]
    public void AddStep_vereist_een_geldige_step()
    {
        var pipeline = new PipelineModel("Build pipeline", PipelineTriggerMode.MANUAL);

        var ex = Assert.Throws<ArgumentNullException>(() => pipeline.AddStep(null!));

        Assert.That(ex!.ParamName, Is.EqualTo("step"));
    }

    [Test]
    public void AddStep_vereist_unieke_order()
    {
        var pipeline = new PipelineModel("Build pipeline", PipelineTriggerMode.MANUAL);

        pipeline.AddStep(new PipelineStep("Sources", 0, PipelineStepType.SOURCES));

        var ex = Assert.Throws<InvalidOperationException>(() =>
            pipeline.AddStep(new PipelineStep("Duplicate", 0, PipelineStepType.BUILD)));

        Assert.That(ex!.Message, Does.StartWith("A step with order 0 already exists."));
    }

    [Test]
    public void CreateExecution_voert_alle_stappen_uit_en_markeert_success()
    {
        var pipeline = new PipelineModel("Build pipeline", PipelineTriggerMode.MANUAL);
        pipeline.AddStep(new PipelineStep("Sources", 0, PipelineStepType.SOURCES));
        pipeline.AddStep(new PipelineStep("Build", 1, PipelineStepType.BUILD));

        var execution = pipeline.CreateExecution();

        Assert.Multiple(() =>
        {
            Assert.That(execution.Status, Is.EqualTo(ExecutionStatus.SUCCEEDED));
            Assert.That(execution.StartedAt, Is.Not.Null);
            Assert.That(execution.FinishedAt, Is.Not.Null);
            Assert.That(execution.StepExecutions, Has.Count.EqualTo(2));
            Assert.That(execution.StepExecutions.Select(step => step.Status), Is.All.EqualTo(ExecutionStatus.SUCCEEDED));
        });
    }
}
