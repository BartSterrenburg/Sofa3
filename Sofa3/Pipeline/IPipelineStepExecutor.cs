using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.Pipeline
{
    public interface IPipelineStepExecutor
    {
        StepExecution Execute(PipelineStep step, PipelineExecution context);
    }
}
