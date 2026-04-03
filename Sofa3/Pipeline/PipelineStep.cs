using Sofa3.Domain.Notification;
using Sofa3.Domain.Pipeline.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.Pipeline
{
    public class PipelineStep
    {
        public Guid StepId { get; }
        public string Name { get; private set; }
        public int Order { get; private set; }
        public PipelineStepType Type { get; private set; }

        public PipelineStep(string name, int order, PipelineStepType type)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Step name is required.", nameof(name));

            if (order < 0)
                throw new ArgumentOutOfRangeException(nameof(order), "Order must be zero or greater.");

            StepId = Guid.NewGuid();
            Name = name;
            Order = order;
            Type = type;
        }

        public StepExecution Execute(PipelineExecution context)
        {
            ArgumentNullException.ThrowIfNull(context);

            var factory = new PipelineStepExecutorFactory();
            var executor = PipelineStepExecutorFactory.Create(Type);

            return executor.Execute(this, context);
        }
    }
}
