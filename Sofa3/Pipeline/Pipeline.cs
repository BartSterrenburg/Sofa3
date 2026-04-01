using Sofa3.Domain.Pipeline.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.Pipeline
{
    public class Pipeline
    {
        private readonly List<PipelineStep> _steps = new();

        public Guid PipelineId { get; }
        public string Name { get; private set; }
        public PipelineTriggerMode TriggerMode { get; private set; }

        public IReadOnlyCollection<PipelineStep> Steps => _steps
            .OrderBy(s => s.Order)
            .ToList()
            .AsReadOnly();

        public Pipeline(string name, PipelineTriggerMode triggerMode)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Pipeline name is required.", nameof(name));

            PipelineId = Guid.NewGuid();
            Name = name;
            TriggerMode = triggerMode;
        }

        public void AddStep(PipelineStep step)
        {
            if (step == null)
                throw new ArgumentNullException(nameof(step));

            if (_steps.Any(s => s.Order == step.Order))
                throw new InvalidOperationException($"A step with order {step.Order} already exists.");

            _steps.Add(step);
        }

        public PipelineExecution CreateExecution()
        {
            var execution = new PipelineExecution();
            execution.Start();

            foreach (var step in _steps.OrderBy(s => s.Order))
            {
                var stepExecution = step.Execute(execution);
                execution.AddStepExecution(stepExecution);

                if (stepExecution.Status == ExecutionStatus.FAILED)
                {
                    execution.MarkFailed();
                    return execution;
                }
            }

            execution.MarkSucceeded();
            return execution;
        }
    }
}
