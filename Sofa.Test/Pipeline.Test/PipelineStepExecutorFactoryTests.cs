using Sofa3.Domain.Pipeline;
using Sofa3.Domain.Pipeline.Enumerations;
using Sofa3.Domain.Pipeline.StepExcecutors;

namespace TestProject1.Pipeline.Test;

public class PipelineStepExecutorFactoryTests
{
    [Test]
    public void Create_geeft_sources_executor_terug_voor_sources()
    {
        Assert.That(PipelineStepExecutorFactory.Create(PipelineStepType.SOURCES), Is.TypeOf<SourcesStepExecutor>());
    }

    [Test]
    public void Create_geeft_package_executor_terug_voor_package()
    {
        Assert.That(PipelineStepExecutorFactory.Create(PipelineStepType.PACKAGE), Is.TypeOf<PackageStepExecutor>());
    }

    [Test]
    public void Create_geeft_build_executor_terug_voor_build()
    {
        Assert.That(PipelineStepExecutorFactory.Create(PipelineStepType.BUILD), Is.TypeOf<BuildStepExecutor>());
    }

    [Test]
    public void Create_geeft_test_executor_terug_voor_test()
    {
        Assert.That(PipelineStepExecutorFactory.Create(PipelineStepType.TEST), Is.TypeOf<TestStepExecutor>());
    }

    [Test]
    public void Create_geeft_analyse_executor_terug_voor_analyse()
    {
        Assert.That(PipelineStepExecutorFactory.Create(PipelineStepType.ANALYSE), Is.TypeOf<AnalyseStepExecutor>());
    }

    [Test]
    public void Create_geeft_deploy_executor_terug_voor_deploy()
    {
        Assert.That(PipelineStepExecutorFactory.Create(PipelineStepType.DEPLOY), Is.TypeOf<DeployStepExecutor>());
    }

    [Test]
    public void Create_geeft_utility_executor_terug_voor_utility()
    {
        Assert.That(PipelineStepExecutorFactory.Create(PipelineStepType.UTILITY), Is.TypeOf<UtilityStepExecutor>());
    }

    [Test]
    public void Create_gooit_NotSupportedException_bij_onbekende_type()
    {
        var ex = Assert.Throws<NotSupportedException>(() => PipelineStepExecutorFactory.Create((PipelineStepType)999));

        Assert.That(ex!.Message, Does.StartWith("Unsupported pipeline step type:"));
    }
}

