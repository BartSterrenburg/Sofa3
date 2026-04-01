using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.Pipeline.StepExcecutors
{
    public class BuildStepExecutor : IPipelineStepExecutor
    {
        public StepExecution Execute(PipelineStep step, PipelineExecution context)
        {
            var result = new StepExecution();
            result.MarkRunning();

            result.SetLogMessage($"Build step '{step.Name}' executed.");
            result.MarkSucceeded();

            return result;
        }
    }
}
