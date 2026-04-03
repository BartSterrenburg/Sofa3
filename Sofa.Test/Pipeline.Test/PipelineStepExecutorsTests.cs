using Sofa3.Domain.Pipeline;
using Sofa3.Domain.Pipeline.Enumerations;
using Sofa3.Domain.Pipeline.StepExcecutors;

namespace TestProject1.Pipeline.Test;

public class PipelineStepExecutorsTests
{
    private static IEnumerable<TestCaseData> Executors()
    {
        yield return new TestCaseData(new SourcesStepExecutor(), PipelineStepType.SOURCES, "Sources step 'Compile sources' executed.")
            .SetName("SourcesStepExecutor_executes_successfully");
        yield return new TestCaseData(new PackageStepExecutor(), PipelineStepType.PACKAGE, "Package step 'Compile sources' executed.")
            .SetName("PackageStepExecutor_executes_successfully");
        yield return new TestCaseData(new BuildStepExecutor(), PipelineStepType.BUILD, "Build step 'Compile sources' executed.")
            .SetName("BuildStepExecutor_executes_successfully");
        yield return new TestCaseData(new TestStepExecutor(), PipelineStepType.TEST, "Test step 'Compile sources' executed.")
            .SetName("TestStepExecutor_executes_successfully");
        yield return new TestCaseData(new AnalyseStepExecutor(), PipelineStepType.ANALYSE, "Analyse step 'Compile sources' executed.")
            .SetName("AnalyseStepExecutor_executes_successfully");
        yield return new TestCaseData(new DeployStepExecutor(), PipelineStepType.DEPLOY, "Deploy step 'Compile sources' executed.")
            .SetName("DeployStepExecutor_executes_successfully");
        yield return new TestCaseData(new UtilityStepExecutor(), PipelineStepType.UTILITY, "Utility step 'Compile sources' executed.")
            .SetName("UtilityStepExecutor_executes_successfully");
    }

    [TestCaseSource(nameof(Executors))]
    public void Execute_returns_successful_step_execution_with_expected_log(IPipelineStepExecutor executor, PipelineStepType type, string expectedLog)
    {
        var step = new PipelineStep("Compile sources", 0, type);
        var context = new PipelineExecution();

        var execution = executor.Execute(step, context);

        Assert.Multiple(() =>
        {
            Assert.That(execution.Status, Is.EqualTo(ExecutionStatus.SUCCEEDED));
            Assert.That(execution.StartedAt, Is.Not.Null);
            Assert.That(execution.FinishedAt, Is.Not.Null);
            Assert.That(execution.LogMessage, Is.EqualTo(expectedLog));
        });
    }
}

