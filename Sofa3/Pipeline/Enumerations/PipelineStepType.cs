using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.Pipeline.Enumerations
{
    public enum PipelineStepType
    {
        SOURCES,
        PACKAGE,
        BUILD,
        TEST,
        ANALYSE,
        DEPLOY,
        UTILITY
    }
}
