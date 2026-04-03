using Sofa3.Domain.Notification;
using Sofa3.Domain.Pipeline.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.Pipeline
{
    public class PipelineExecution
    {
        private readonly List<StepExecution> _stepExecutions = new();

        public Guid ExecutionId { get; }
        public DateTime? StartedAt { get; private set; }
        public DateTime? FinishedAt { get; private set; }
        public ExecutionStatus Status { get; private set; }

        public IReadOnlyCollection<StepExecution> StepExecutions => _stepExecutions.AsReadOnly();

        public PipelineExecution()
        {
            ExecutionId = Guid.NewGuid();
            Status = ExecutionStatus.PENDING;
        }

        public void Start()
        {
            StartedAt = DateTime.UtcNow;
            Status = ExecutionStatus.RUNNING;
        }

        public void AddStepExecution(StepExecution stepExecution)
        {
            ArgumentNullException.ThrowIfNull(stepExecution);

            _stepExecutions.Add(stepExecution);
        }

        public void MarkSucceeded()
        {
            Status = ExecutionStatus.SUCCEEDED;
            FinishedAt = DateTime.UtcNow;
        }

        public void MarkFailed()
        {
            Status = ExecutionStatus.FAILED;
            FinishedAt = DateTime.UtcNow;
        }

        public void Cancel()
        {
            Status = ExecutionStatus.CANCELLED;
            FinishedAt = DateTime.UtcNow;
        }
    }
}
