using Sofa3.Domain.Pipeline.Enumerations;
using Sofa3.Domain.Pipeline.StepExcecutors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.Pipeline
{
    public class PipelineStepExecutorFactory
    {
        public IPipelineStepExecutor Create(PipelineStepType type)
        {
            return type switch
            {
                PipelineStepType.SOURCES => new SourcesStepExecutor(),
                PipelineStepType.PACKAGE => new PackageStepExecutor(),
                PipelineStepType.BUILD => new BuildStepExecutor(),
                PipelineStepType.TEST => new TestStepExecutor(),
                PipelineStepType.ANALYSE => new AnalyseStepExecutor(),
                PipelineStepType.DEPLOY => new DeployStepExecutor(),
                PipelineStepType.UTILITY => new UtilityStepExecutor(),
                _ => throw new NotSupportedException($"Unsupported pipeline step type: {type}")
            };
        }
    }
}
