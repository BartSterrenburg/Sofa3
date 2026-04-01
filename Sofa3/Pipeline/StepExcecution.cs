using Sofa3.Domain.Pipeline.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.Pipeline
{
    public class StepExecution
    {
        public Guid StepExecutionId { get; }
        public DateTime? StartedAt { get; private set; }
        public DateTime? FinishedAt { get; private set; }
        public ExecutionStatus Status { get; private set; }
        public string LogMessage { get; private set; }

        public StepExecution()
        {
            StepExecutionId = Guid.NewGuid();
            Status = ExecutionStatus.PENDING;
            LogMessage = string.Empty;
        }

        public void MarkRunning()
        {
            StartedAt = DateTime.UtcNow;
            Status = ExecutionStatus.RUNNING;
        }

        public void MarkSucceeded()
        {
            FinishedAt = DateTime.UtcNow;
            Status = ExecutionStatus.SUCCEEDED;
        }

        public void MarkFailed()
        {
            FinishedAt = DateTime.UtcNow;
            Status = ExecutionStatus.FAILED;
        }

        public void SetLogMessage(string logMessage)
        {
            LogMessage = logMessage ?? string.Empty;
        }
    }
}
