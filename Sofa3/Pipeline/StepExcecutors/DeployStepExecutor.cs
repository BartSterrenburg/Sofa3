using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.Pipeline.StepExcecutors
{
    public class DeployStepExecutor : IPipelineStepExecutor
    {
        public StepExecution Execute(PipelineStep step, PipelineExecution context)
        {
            var result = new StepExecution();
            result.MarkRunning();

            result.SetLogMessage($"Deploy step '{step.Name}' executed.");
            result.MarkSucceeded();

            return result;
        }
    }
}
